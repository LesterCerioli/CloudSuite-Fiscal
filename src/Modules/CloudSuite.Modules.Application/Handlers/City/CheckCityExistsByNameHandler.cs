using CloudSuite.Modules.Application.Handlers.City.Requests;
using CloudSuite.Modules.Application.Handlers.City.Responses;
using CloudSuite.Modules.Application.Handlers.FederalTax;
using CloudSuite.Modules.Application.Handlers.FederalTax.Responses;
using CloudSuite.Modules.Application.Validations.City;
using CloudSuite.Modules.Application.Validations.FederalTax;
using CloudSuite.Modules.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.City
{
    public class CheckCityExistsByNameHandler : IRequestHandler<CheckCityExistsByNameRequest, CheckCityExistsByNameResponse>
    {
        private readonly ICityRepository  _cityRepository;
        private readonly ILogger<CheckCityExistsByNameHandler> _logger;

        public CheckCityExistsByNameHandler(ICityRepository cityRepository, ILogger<CheckCityExistsByNameHandler> logger)
        {
            _cityRepository = cityRepository;
            _logger = logger;
        }
        public async Task<CheckCityExistsByNameResponse> Handle(CheckCityExistsByNameRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckFederalTaxExistsByVpisRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckCityExistsByNameRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var alimony = await _cityRepository.GetByCityName(request.CityName);

                    if (alimony != null)
                    {
                        return await Task.FromResult(new CheckCityExistsByNameResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckCityExistsByNameResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckCityExistsByNameResponse(request.Id, false, validationResult));

        }
    }
}
