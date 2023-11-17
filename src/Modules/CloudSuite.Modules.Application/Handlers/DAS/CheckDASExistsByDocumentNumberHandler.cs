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
    public class CheckDASExistsByDocumentNumberHandler : IRequestHandler<CheckDASExistsByDocumentNumberRequest, CheckDASExistsByDocumentNumberResponse>
    {
        private readonly IDASRepository _dasRepository;
        private readonly ILogger<CheckDASExistsByDueDateHandler> _logger;

        public CheckDASExistsByDocumentNumberHandler(IDASRepository dasRepository, ILogger<CheckDASExistsByDueDateHandler> logger)
        {
            _dasRepository = dasRepository;
            _logger = logger;
        }
        public async Task<CheckDASExistsByDocumentNumberResponse> Handle(CheckDASExistsByDocumentNumberRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckDASExistsByDocumentNumberRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckDASExistsByDocumentNumberRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var cityName = await _dasRepository.GetByDocumentNumber(request.DocumentNumber);

                    if (cityName != null)
                    {
                        return await Task.FromResult(new CheckDASExistsByDocumentNumberResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckDASExistsByDocumentNumberResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckDASExistsByDocumentNumberResponse(request.Id, false, validationResult));

        }
    }
}
