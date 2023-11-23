using CloudSuite.Modules.Application.Handlers.Address;
using CloudSuite.Modules.Application.Handlers.Address.Responses;
using CloudSuite.Modules.Application.Handlers.CancelOrder.Responses;
using CloudSuite.Modules.Application.Validations.Address;
using CloudSuite.Modules.Application.Validations.CancelOrder;
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

namespace CloudSuite.Modules.Application.Handlers.CancelOrder
{
    public class CreateCancelOrderHandler : IRequestHandler<CreateCancelOrderCommand, CreateCancelOrderResponse>
    {
        private readonly ICancelOrderRepository _cancelOrderRepository;
        private readonly ILogger<CreateCancelOrderHandler> _logger;

        public CreateCancelOrderHandler(ICancelOrderRepository cancelOrderRepository, ILogger<CreateCancelOrderHandler> logger)
        {
            _cancelOrderRepository = cancelOrderRepository;
            _logger = logger;
        }
        public async Task<CreateCancelOrderResponse> Handle(CreateCancelOrderCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateCancelOrderCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateCancelOrderCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var cnpj = await _cancelOrderRepository.GetbyCnpj(command.Cnpj);
                    var requestDate = await _cancelOrderRepository.GetByRequestDate(command.RequestDate);

                    if (cnpj == null && requestDate == null)
                    {
                        await _cancelOrderRepository.Add(command.GetEntity());
                        return new CreateCancelOrderResponse(command.Id, validationResult);
                    }

                    return new CreateCancelOrderResponse(command.Id, "Address already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating extract");
                    return new CreateCancelOrderResponse(command.Id, "Error creating Adress");
                }
            }
            return new CreateCancelOrderResponse(command.Id, validationResult);
        }
    }
}
