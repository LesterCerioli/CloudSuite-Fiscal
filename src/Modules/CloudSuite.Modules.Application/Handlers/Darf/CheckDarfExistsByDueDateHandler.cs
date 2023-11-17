using CloudSuite.Modules.Application.Handlers.Darf.Requests;
using CloudSuite.Modules.Application.Handlers.Darf.Responses;
using CloudSuite.Modules.Application.Validations.Darf;
using CloudSuite.Modules.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Darf
{
    public class CheckDarfExistsByDueDateHandler : IRequestHandler<CheckDarfExistsByDueDateRequest, CheckDarfExistsByDueDateResponse>
    {
        private readonly IDarfRepository _darfRepository;
        private readonly ILogger<CheckDarfExistsByReferenceMonthHandler> _logger;

        public CheckDarfExistsByDueDateHandler(IDarfRepository darfRepository, ILogger<CheckDarfExistsByReferenceMonthHandler> logger)
        {
            _darfRepository = darfRepository;
            _logger = logger;
        }
        public async Task<CheckDarfExistsByDueDateResponse> Handle(CheckDarfExistsByDueDateRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckDarfExistsByDueDateRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckDarfExistsByDueDateRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var cityName = await _darfRepository.GetByDueDate(request.DueDate);

                    if (cityName != null)
                    {
                        return await Task.FromResult(new CheckDarfExistsByDueDateResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckDarfExistsByDueDateResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckDarfExistsByDueDateResponse(request.Id, false, validationResult));

        }
    }
}
