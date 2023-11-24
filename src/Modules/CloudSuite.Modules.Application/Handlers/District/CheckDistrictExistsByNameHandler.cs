using CloudSuite.Modules.Application.Handlers.DeclaracaoIR;
using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using CloudSuite.Modules.Application.Handlers.District.Requests;
using CloudSuite.Modules.Application.Handlers.District.Responses;
using CloudSuite.Modules.Application.Validations.DeclaracaoIR;
using CloudSuite.Modules.Application.Validations.District;
using CloudSuite.Modules.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.District
{
    public class CheckDistrictExistsByNameHandler : IRequestHandler<CheckDistrictExistsByNameRequest, CheckDistrictExistsByNameResponse>
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly ILogger<CheckDistrictExistsByNameHandler> _logger;

        public CheckDistrictExistsByNameHandler(IDistrictRepository districtRepository, ILogger<CheckDistrictExistsByNameHandler> logger)
        {
            _districtRepository = districtRepository;
            _logger = logger;
        }
        public async Task<CheckDistrictExistsByNameResponse> Handle(CheckDistrictExistsByNameRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckDistrictExistsByNameRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckDistrictExistsByNameRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var districtName = await _districtRepository.GetByName(request.Name);

                    if (districtName != null)
                    {
                        return await Task.FromResult(new CheckDistrictExistsByNameResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckDistrictExistsByNameResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckDistrictExistsByNameResponse(request.Id, false, validationResult));

        }
    }
}
