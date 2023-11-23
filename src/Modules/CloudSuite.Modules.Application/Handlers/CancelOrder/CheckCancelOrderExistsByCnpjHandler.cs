using CloudSuite.Modules.Application.Handlers.Address;
using CloudSuite.Modules.Application.Handlers.CancelOrder.Requests;
using CloudSuite.Modules.Application.Handlers.CancelOrder.Responses;
using CloudSuite.Modules.Application.Handlers.FederalTax.Responses;
using CloudSuite.Modules.Application.Validations.CancelOrder;
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

namespace CloudSuite.Modules.Application.Handlers.CancelOrder
{
    public class CheckCancelOrderExistsByCnpjHandler : IRequestHandler<CheckCancelOrderExistsByCnpjRequest, CheckCancelOrderExistsByCnpjResponse>
    {
        private readonly ICancelOrderRepository _cancelOrderRepository;
        private readonly ILogger<CreateAddressHandler> _logger;

        public CheckCancelOrderExistsByCnpjHandler(ICancelOrderRepository cancelOrderRepository, ILogger<CreateAddressHandler> logger)
        {
            _cancelOrderRepository = cancelOrderRepository;
            _logger = logger;
        }
        public async Task<CheckCancelOrderExistsByCnpjResponse> Handle(CheckCancelOrderExistsByCnpjRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckCancelOrderExistsByCnpjRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckCancelOrderExistsByCnpjRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var cnpj = await _cancelOrderRepository.GetbyCnpj(request.Cnpj);

                    if (cnpj != null)
                    {
                        return await Task.FromResult(new CheckCancelOrderExistsByCnpjResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckCancelOrderExistsByCnpjResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckCancelOrderExistsByCnpjResponse(request.Id, false, validationResult));

        }
    }
}
