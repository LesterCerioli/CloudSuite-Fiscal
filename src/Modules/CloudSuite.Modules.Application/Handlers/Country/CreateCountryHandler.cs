using CloudSuite.Modules.Application.Handlers.Address;
using CloudSuite.Modules.Application.Handlers.Address.Responses;
using CloudSuite.Modules.Application.Handlers.Country.Responses;
using CloudSuite.Modules.Application.Validations.Address;
using CloudSuite.Modules.Application.Validations.Country;
using CloudSuite.Modules.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Country
{
    public class CreateCountryHandler : IRequestHandler<CreateCountryCommand, CreateCountryResponse>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILogger<CreateCountryHandler> _logger;

        public CreateCountryHandler(ICountryRepository countryRepository, ILogger<CreateCountryHandler> logger)
        {
            _countryRepository = countryRepository;
            _logger = logger;
        }
        public async Task<CreateCountryResponse> Handle(CreateCountryCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateCountryCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateCountryCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var countryName = await _countryRepository.GetbyCountryName(command.CountryName);

                    if (countryName == null)
                    {
                        await _countryRepository.Add(command.GetEntity());
                        return new CreateCountryResponse(command.Id, validationResult);
                    }

                    return new CreateCountryResponse(command.Id, "Country already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating Country");
                    return new CreateCountryResponse(command.Id, "Error creating Country");
                }
            }
            return new CreateCountryResponse(command.Id, validationResult);
        }
    }
}
