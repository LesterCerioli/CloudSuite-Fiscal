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
    public class CheckDASExistsByDueDateHandler : IRequestHandler<CheckDASExistsByDueDateRequest, CheckDASExistsByDueDateResponse>
    {
        private readonly IDASRepository _dasRepository;
        private readonly ILogger<CheckDASExistsByDueDateHandler> _logger;

        public CheckDASExistsByDueDateHandler(IDASRepository dasRepository, ILogger<CheckDASExistsByDueDateHandler> logger)
        {
            _dasRepository = dasRepository;
            _logger = logger;
        }

        public async Task<CheckDASExistsByDueDateResponse> Handle(CheckDASExistsByDueDateRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckDASExistsByDueDateRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckDASExistsByDueDateRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var dueDate = await _dasRepository.GetByDueDate(request.DueDate);

                    if (dueDate != null)
                    {
                        return await Task.FromResult(new CheckDASExistsByDueDateResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckDASExistsByDueDateResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckDASExistsByDueDateResponse(request.Id, false, validationResult));

        }
    }
}
