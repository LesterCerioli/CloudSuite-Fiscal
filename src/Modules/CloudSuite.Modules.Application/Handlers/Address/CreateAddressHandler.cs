using CloudSuite.Modules.Application.Handlers.Address.Responses;
using CloudSuite.Modules.Application.Validations.Address;
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

namespace CloudSuite.Modules.Application.Handlers.Address
{
    public class CreateAddressHandler : IRequestHandler<CreateAddressCommand, CreateAddressResponse>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<CreateAddressHandler> _logger;

        public CreateAddressHandler(IAddressRepository adressRepository, ILogger<CreateAddressHandler> logger)
        {
            _addressRepository = adressRepository;
            _logger = logger;
        }

        public async Task<CreateAddressResponse> Handle(CreateAddressCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateAddressCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateAddressCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var adressExistAdressLine = await _addressRepository.GetByAddressLine1(command.AddressLine1);

                    if (adressExistAdressLine == null)
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
