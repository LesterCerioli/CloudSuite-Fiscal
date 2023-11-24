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
    public class CheckTomadorServicoBySocialReasonHandler : IRequestHandler<CheckTomadorServicoBySocialReasonRequest, CheckTomadorServicoBySocialReasonResponse>
    {
        private readonly ITomadorServicoRepository _tomadorServicoRepository;
        private readonly ILogger<CheckTomadorServicoBySocialReasonHandler> _logger;

        public CheckTomadorServicoBySocialReasonHandler(ITomadorServicoRepository tomadorServicoRepository, ILogger<CheckTomadorServicoBySocialReasonHandler> logger)
        {
            _tomadorServicoRepository = tomadorServicoRepository;
            _logger = logger;
        }
        public async Task<CheckTomadorServicoBySocialReasonResponse> Handle(CheckTomadorServicoBySocialReasonRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckTomadorServicoBySocialReasonRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckTomadorServicoBySocialReasonRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var socialReason = await _tomadorServicoRepository.GetBySocialReason(request.SocialReason);

                    if (socialReason != null)
                    {
                        return await Task.FromResult(new CheckTomadorServicoBySocialReasonResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckTomadorServicoBySocialReasonResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckTomadorServicoBySocialReasonResponse(request.Id, false, validationResult));

        }
    }
}
