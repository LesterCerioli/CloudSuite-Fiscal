﻿using CloudSuite.Modules.Application.Handlers.DeclaracaoIR;
using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using CloudSuite.Modules.Application.Handlers.Prestador.Requests;
using CloudSuite.Modules.Application.Handlers.Prestador.Responses;
using CloudSuite.Modules.Application.Validations.DeclaracaoIR;
using CloudSuite.Modules.Application.Validations.Prestador;
using CloudSuite.Modules.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Prestador
{
    public class CheckPrestadorExistsByCnpjHandler : IRequestHandler<CheckPrestadorExistsByCnpjRequest, CheckPrestadorExistsByCnpjResponse>
    {
        private readonly IPrestadorRepository _prestadorRepository;
        private readonly ILogger<CheckPrestadorExistsByCnpjHandler> _logger;

        public CheckPrestadorExistsByCnpjHandler(IPrestadorRepository prestadorRepository, ILogger<CheckPrestadorExistsByCnpjHandler> logger)
        {
            _prestadorRepository = prestadorRepository;
            _logger = logger;
        }

        public async Task<CheckPrestadorExistsByCnpjResponse> Handle(CheckPrestadorExistsByCnpjRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckPrestadorExistsByCnpjRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckPrestadorExistsByCnpjRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var cnpj = await _prestadorRepository.GetByCnpj(request.Cnpj);

                    if (cnpj != null)
                    {
                        return await Task.FromResult(new CheckPrestadorExistsByCnpjResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckPrestadorExistsByCnpjResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckPrestadorExistsByCnpjResponse(request.Id, false, validationResult));

        }
    }
}
