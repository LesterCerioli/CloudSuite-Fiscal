using CloudSuite.Modules.Application.Handlers.Address.Responses;
using CloudSuite.Modules.Application.Validations.Address;
using MediatR;
using Microsoft.Extensions.Logging;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Address
{
    public class CreateAddressHandler : IRequestHandler<CreateAddressCommand, CreateAddressResponse>
    {
        private readonly IAdressRepository _adressRepository;
        private readonly ILogger<CreateAddressHandler> _logger;

        public CreateAddressHandler(IAdressRepository adressRepository, ILogger<CreateAddressHandler> logger)
        {
            _adressRepository = adressRepository;
            _logger = logger;
        }

        public async Task<CreateAddressResponse> Handle(CreateAddressCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateExtractCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateAddressCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var adressExistAdressLine = await _addressRepository.GetByAddressLine(command.AddressLine1);
                    var adressExistContactName = await _addressRepository.GetByContactName(command.ContactName);

                    if (adressExistAdressLine == null && adressExistContactName == null)
                    {
                        await _addressRepository.Add(command.GetEntity());
                        return new CreateAddressResponse(command.Id, validationResult);
                    }

                    return new CreateAddressResponse(command.Id, "Address already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating extract");
                    return new CreateAddressResponse(command.Id, "Error creating Adress");
                }
            }
            return new CreateAddressResponse(command.Id, validationResult);
        }
    }
}
