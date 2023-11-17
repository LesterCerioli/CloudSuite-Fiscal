using CloudSuite.Modules.Application.Handlers.Address;
using CloudSuite.Modules.Application.Handlers.Address.Responses;
using CloudSuite.Modules.Application.Handlers.Country.Responses;
using CloudSuite.Modules.Application.Validations.Address;
using CloudSuite.Modules.Application.Validations.Country;
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
        public Task<CreateCountryResponse> Handle(CreateCountryCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateCountryCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateCountryCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var adressExistAdressLine = await _countryRepository.GetByAddressLine(command.AddressLine1);
                    var adressExistContactName = await _countryRepository.GetByContactName(command.ContactName);

                    if (adressExistAdressLine == null && adressExistContactName == null)
                    {
                        await _countryRepository.Add(command.GetEntity());
                        return new CreateCountryResponse(command.Id, validationResult);
                    }

                    return new CreateCountryResponse(command.Id, "Address already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating extract");
                    return new CreateCountryResponse(command.Id, "Error creating Adress");
                }
            }
            return new CreateCountryResponse(command.Id, validationResult);
        }
    }
}
