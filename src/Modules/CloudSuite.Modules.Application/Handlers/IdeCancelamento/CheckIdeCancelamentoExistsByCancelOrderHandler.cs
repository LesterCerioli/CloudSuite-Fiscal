using CloudSuite.Modules.Application.Handlers.Darf.Responses;
using CloudSuite.Modules.Application.Handlers.IdeCancelamento.Requests;
using CloudSuite.Modules.Application.Handlers.IdeCancelamento.Responses;
using CloudSuite.Modules.Application.Validations.Darf;
using CloudSuite.Modules.Application.Validations.IdeCancelamento;
using CloudSuite.Modules.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.IdeCancelamento
{
    public class CheckIdeCancelamentoExistsByCancelOrderHandler : IRequestHandler<CheckIdeCancelamentoExistsByCancelOrderRequest, CheckIdeCancelamentoExistsByCancelOrderResponse>
    {
        private readonly IdeCancelamentoRepository _ideCancelamentoRepository;
        private readonly ILogger<CheckIdeCancelamentoExistsByCancelOrderHandler> _logger;

        public CheckIdeCancelamentoExistsByCancelOrderHandler(IdeCancelamentoRepository ideCancelamentoRepository, ILogger<CheckIdeCancelamentoExistsByCancelOrderHandler> logger)
        {
            _ideCancelamentoRepository = ideCancelamentoRepository;
            _logger = logger;
        }

        public async Task<CheckIdeCancelamentoExistsByCancelOrderResponse> Handle(CheckIdeCancelamentoExistsByCancelOrderRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckIdeCancelamentoExistsByCancelOrderRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckIdeCancelamentoExistsByCancelOrderRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var cityName = await _ideCancelamentoRepository.GetByCancelOrder(request.CancelOrder);

                    if (cityName != null)
                    {
                        return await Task.FromResult(new CheckIdeCancelamentoExistsByCancelOrderResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckIdeCancelamentoExistsByCancelOrderResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckIdeCancelamentoExistsByCancelOrderResponse(request.Id, false, validationResult));
        }
    }
}
