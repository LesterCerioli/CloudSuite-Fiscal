using CloudSuite.Modules.Application.Handlers.Address;
using CloudSuite.Modules.Application.Handlers.Address.Responses;
using CloudSuite.Modules.Application.Handlers.City.Responses;
using CloudSuite.Modules.Application.Validations.Address;
using CloudSuite.Modules.Application.Validations.City;
using MediatR;
using Microsoft.Extensions.Logging;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.City
{
    public class CreateCityHandler : IRequestHandler<CreateCityCommand, CreateCityResponse>
    {
        private readonly ICityRepository _cityRepository;
        private readonly ILogger<CreateCityHandler> _logger;

        public CreateCityHandler(ICityRepository cityRepository, ILogger<CreateCityHandler> logger)
        {
            _cityRepository = cityRepository;
            _logger = logger;
        }

        public Task<CreateCityResponse> Handle(CreateCityCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateExtractCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateCityCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var adressExistAdressLine = await _cityRepository.GetBy(command.AddressLine1);
                    var adressExistContactName = await _cityRepository.GetBy(command.ContactName);

                    if (adressExistAdressLine == null && adressExistContactName == null)
                    {
                        await _addressRepository.Add(command.GetEntity());
                        return new CreateCityResponse(command.Id, validationResult);
                    }

                    return new CreateCityResponse(command.Id, "Address already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating extract");
                    return new CreateCityResponse(command.Id, "Error creating Adress");
                }
            }
            return new CreateCityResponse(command.Id, validationResult);
        }
    }
}
