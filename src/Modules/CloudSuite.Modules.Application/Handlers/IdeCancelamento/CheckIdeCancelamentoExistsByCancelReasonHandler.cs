﻿using CloudSuite.Modules.Application.Handlers.IdeCancelamento.Requests;
using CloudSuite.Modules.Application.Handlers.IdeCancelamento.Responses;
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
    public class CheckIdeCancelamentoExistsByCancelReasonHandler : IRequestHandler<CheckIdeCancelamentoExistsByCancelReasonRequest, CheckIdeCancelamentoExistsByCancelReasonResponse>
    {
        private readonly IIdeCancelamentoRepository _ideCancelamentoRepository;
        private readonly ILogger<CheckIdeCancelamentoExistsByCancelReasonHandler> _logger;

        public CheckIdeCancelamentoExistsByCancelReasonHandler(IIdeCancelamentoRepository ideCancelamentoRepository, ILogger<CheckIdeCancelamentoExistsByCancelReasonHandler> logger)
        {
            _ideCancelamentoRepository = ideCancelamentoRepository;
            _logger = logger;
        }
        public async Task<CheckIdeCancelamentoExistsByCancelReasonResponse> Handle(CheckIdeCancelamentoExistsByCancelReasonRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckIdeCancelamentoExistsByCancelReasonRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckIdeCancelamentoExistsByCancelReasonRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var cityName = await _ideCancelamentoRepository.GetByCancelReason(request.CancelReason);

                    if (cityName != null)
                    {
                        return await Task.FromResult(new CheckIdeCancelamentoExistsByCancelReasonResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckIdeCancelamentoExistsByCancelReasonResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckIdeCancelamentoExistsByCancelReasonResponse(request.Id, false, validationResult));
        }
    }
}
