using CloudSuite.Modules.Application.Handlers.CancelOrder.Requests;
using CloudSuite.Modules.Application.Handlers.CancelOrder.Responses;
using CloudSuite.Modules.Application.Handlers.FederalTax;
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
    public class CheckCancelOrderExistsByRequestDateHandler : IRequestHandler<CheckCancelOrderExistsByRequestDateRequest, CheckCancelOrderExistsByRequestDateResponse>
    {
        private readonly ICancelOrderRepository _cancelOrderRepository;
        private readonly ILogger<CheckCancelOrderExistsByRequestDateHandler> _logger;

        public CheckCancelOrderExistsByRequestDateHandler(ICancelOrderRepository cancelOrderRepository, ILogger<CheckCancelOrderExistsByRequestDateHandler> logger)
        {
            _cancelOrderRepository = cancelOrderRepository;
            _logger = logger;
        }
        public async Task<CheckCancelOrderExistsByRequestDateResponse> Handle(CheckCancelOrderExistsByRequestDateRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckCancelOrderExistsByRequestDateRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckCancelOrderExistsByRequestDateRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var alimony = await _cancelOrderRepository.GetByRequestDate(request.RequestDate);

                    if (alimony != null)
                    {
                        return await Task.FromResult(new CheckCancelOrderExistsByRequestDateResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckCancelOrderExistsByRequestDateResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckCancelOrderExistsByRequestDateResponse(request.Id, false, validationResult));

        }
    }
}
