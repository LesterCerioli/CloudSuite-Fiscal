using CloudSuite.Modules.Application.Handlers.Country.Requests;
using CloudSuite.Modules.Application.Handlers.Country.Responses;
using CloudSuite.Modules.Application.Handlers.FederalTax;
using CloudSuite.Modules.Application.Handlers.FederalTax.Responses;
using CloudSuite.Modules.Application.Validations.Country;
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

namespace CloudSuite.Modules.Application.Handlers.Country
{
    public class CheckCountryExistsByNameHandler : IRequestHandler<CheckCountryExistsByNameRequest, CheckCountryExistsByNameResponse>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILogger<CheckFederalTaxExistsByVpisHandler> _logger;

        public CheckCountryExistsByNameHandler(ICountryRepository countryRepository, ILogger<CheckFederalTaxExistsByVpisHandler> logger)
        {
            _countryRepository = countryRepository;
            _logger = logger;
        }
        public async Task<CheckCountryExistsByNameResponse> Handle(CheckCountryExistsByNameRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckCountryExistsByNameRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckCountryExistsByNameRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var country = await _countryRepository.GetbyCountryName(request.CountryName);

                    if (country != null)
                    {
                        return await Task.FromResult(new CheckCountryExistsByNameResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckCountryExistsByNameResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckCountryExistsByNameResponse(request.Id, false, validationResult));

        }
    }
}
