using CloudSuite.Modules.Application.Handlers.Prestador;
using CloudSuite.Modules.Application.Handlers.Prestador.Responses;
using CloudSuite.Modules.Application.Handlers.TomadorServico.Requests;
using CloudSuite.Modules.Application.Handlers.TomadorServico.Responses;
using CloudSuite.Modules.Application.Validations.Prestador;
using CloudSuite.Modules.Application.Validations.TomadorServico;
using CloudSuite.Modules.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.TomadorServico
{
    public class CheckTomadorServicoByCnpjHandler : IRequestHandler<CheckTomadorServicoByCnpjRequest, CheckTomadorServicoByCnpjResponse>
    {
        private readonly IPrestadorRepository _prestadorRepository;
        private readonly ILogger<CheckPrestadorExistsByCnpjHandler> _logger;

        public CheckTomadorServicoByCnpjHandler(IPrestadorRepository prestadorRepository, ILogger<CheckPrestadorExistsByCnpjHandler> logger)
        {
            _prestadorRepository = prestadorRepository;
            _logger = logger;
        }
        public async Task<CheckTomadorServicoByCnpjResponse> Handle(CheckTomadorServicoByCnpjRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckTomadorServicoByCnpjRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckTomadorServicoByCnpjRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var cnpj = await _prestadorRepository.GetByCnpj(request.Cnpj);

                    if (cnpj != null)
                    {
                        return await Task.FromResult(new CheckTomadorServicoByCnpjResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckTomadorServicoByCnpjResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckTomadorServicoByCnpjResponse(request.Id, false, validationResult));

        }
    }
}
