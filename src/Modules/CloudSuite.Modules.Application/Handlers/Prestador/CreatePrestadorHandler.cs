using CloudSuite.Modules.Application.Handlers.DeclaracaoIR;
using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using CloudSuite.Modules.Application.Handlers.Prestador.Responses;
using CloudSuite.Modules.Application.Validations.DeclaracaoIR;
using CloudSuite.Modules.Application.Validations.Prestador;
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

namespace CloudSuite.Modules.Application.Handlers.Prestador
{
    public class CreatePrestadorHandler : IRequestHandler<CreatePrestadorCommand, CreatePrestadorResponse>
    {
        private IPrestadorRepository _prestadorRepository;
        private ILogger<CreatePrestadorHandler> _logger;

        public CreatePrestadorHandler(IPrestadorRepository prestadorRepository, ILogger<CreatePrestadorHandler> logger)
        {
            _prestadorRepository = prestadorRepository;
            _logger = logger;
        }

        public async Task<CreatePrestadorResponse> Handle(CreatePrestadorCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreatePrestadorCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreatePrestadorCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var cnpj = await _prestadorRepository.GetByCnpj(command.Cnpj);
                    var inscricaoMunicipal = await _prestadorRepository.GetByInscricaoMunicipal(command.InscricaoMunicipal);
                    var docestrangeiro = await _prestadorRepository.GetByDocTomadorEstrangeiro(command.DocTomadorEstrangeiro);
                    var nomeFantasia = await _prestadorRepository.GetByNomeFantasia(command.NomeFantasia);
                    var inscricaoEstudal = await _prestadorRepository.GetByInscricaoEstadual(command.InscricaoEstadual);


                    if (cnpj == null && inscricaoMunicipal == null && docestrangeiro == null && nomeFantasia == null && inscricaoEstudal == null)
                    {
                        await _prestadorRepository.Add(command.GetEntity());
                        return new CreatePrestadorResponse(command.Id, validationResult);
                    }

                    return new CreatePrestadorResponse(command.Id, "Prestador already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating Prestador");
                    return new CreatePrestadorResponse(command.Id, "Error creating Prestador");
                }
            }
            return new CreatePrestadorResponse(command.Id, validationResult);
        }
    }
}
