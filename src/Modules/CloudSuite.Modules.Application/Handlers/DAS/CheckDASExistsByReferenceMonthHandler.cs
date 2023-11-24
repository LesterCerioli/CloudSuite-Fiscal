using CloudSuite.Modules.Application.Handlers.Darf.Responses;
using CloudSuite.Modules.Application.Handlers.DAS.Requests;
using CloudSuite.Modules.Application.Handlers.DAS.Responses;
using CloudSuite.Modules.Application.Validations.Darf;
using CloudSuite.Modules.Application.Validations.DAS;
using CloudSuite.Modules.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.DAS
{
    public class CheckDASExistsByReferenceMonthHandler : IRequestHandler<CheckDASExistsByReferenceMonthRequest, CheckDASExistsByReferenceMonthResponse>
    {
        private readonly IDASRepository _dasRepository;
        private readonly ILogger<CheckDASExistsByReferenceMonthHandler> _logger;

        public CheckDASExistsByReferenceMonthHandler(IDASRepository dasRepository, ILogger<CheckDASExistsByReferenceMonthHandler> logger)
        {
            _dasRepository = dasRepository;
            _logger = logger;
        }

        public async Task<CheckDASExistsByReferenceMonthResponse> Handle(CheckDASExistsByReferenceMonthRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckDASExistsByReferenceMonthRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckDASExistsByReferenceMonthRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var referenceMonth = await _dasRepository.GetByReferenceMonth(request.ReferenceMonth);

                    if (referenceMonth != null)
                    {
                        return await Task.FromResult(new CheckDASExistsByReferenceMonthResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckDASExistsByReferenceMonthResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckDASExistsByReferenceMonthResponse(request.Id, false, validationResult));

        }
    }
}
