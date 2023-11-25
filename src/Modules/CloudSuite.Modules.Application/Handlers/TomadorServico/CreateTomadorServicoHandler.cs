using CloudSuite.Modules.Application.Handlers.Prestador;
using CloudSuite.Modules.Application.Handlers.Prestador.Responses;
using CloudSuite.Modules.Application.Handlers.TomadorServico.Responses;
using CloudSuite.Modules.Application.Validations.Prestador;
using CloudSuite.Modules.Application.Validations.TomadorServico;
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

namespace CloudSuite.Modules.Application.Handlers.TomadorServico
{
    public class CreateTomadorServicoHandler : IRequestHandler<CreateTomadorServicoCommand, CreateTomadorServicoResponse>
    {
        private ITomadorServicoRepository _tomadorServicoRepository;
        private ILogger<CreateTomadorServicoHandler> _logger;

        public CreateTomadorServicoHandler(ITomadorServicoRepository tomadorServicoRepository, ILogger<CreateTomadorServicoHandler> logger)
        {
            _tomadorServicoRepository = tomadorServicoRepository;
            _logger = logger;
        }
        public async Task<CreateTomadorServicoResponse> Handle(CreateTomadorServicoCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateTomadorServicoCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateTomadorServicoCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var cnpj = await _tomadorServicoRepository.GetByCnpj(command.Cnpj);
                    var socialReason = await _tomadorServicoRepository.GetBySocialReason(command.SocialReason);
                   

                    if (cnpj == null && socialReason == null)
                    {
                        await _tomadorServicoRepository.Add(command.GetEntity());
                        return new CreateTomadorServicoResponse(command.Id, validationResult);
                    }

                    return new CreateTomadorServicoResponse(command.Id, "TomadorServico already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating TomadorServico");
                    return new CreateTomadorServicoResponse(command.Id, "Error creating TomadorServico");
                }
            }
            return new CreateTomadorServicoResponse(command.Id, validationResult);

        }
    }
}
