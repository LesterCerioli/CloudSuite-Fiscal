using CloudSuite.Modules.Application.Handlers.DAS.Requests;
using CloudSuite.Modules.Application.Handlers.DAS.Responses;
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
    public class CheckDASExistsByReferenceYearHandler : IRequestHandler<CheckDASExistsByReferenceYearRequest, CheckDASExistsByReferenceYearResponse>
    {
        private readonly IDASRepository _dasRepository;
        private readonly ILogger<CheckDASExistsByReferenceMonthHandler> _logger;

        public CheckDASExistsByReferenceYearHandler(IDASRepository dasRepository, ILogger<CheckDASExistsByReferenceMonthHandler> logger)
        {
            _dasRepository = dasRepository;
            _logger = logger;
        }
        public async Task<CheckDASExistsByReferenceYearResponse> Handle(CheckDASExistsByReferenceYearRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckDASExistsByReferenceYearRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckDASExistsByReferenceYearRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var referenceYear = await _dasRepository.GetByReferenceYear(request.ReferenceYear);

                    if (referenceYear != null)
                    {
                        return await Task.FromResult(new CheckDASExistsByReferenceYearResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckDASExistsByReferenceYearResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckDASExistsByReferenceYearResponse(request.Id, false, validationResult));

        }
    }
}
