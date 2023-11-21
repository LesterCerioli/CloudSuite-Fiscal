using CloudSuite.Modules.Application.Handler.Responses;
using CloudSuite.Modules.Application.Validation;
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

namespace CloudSuite.Modules.Application.Handler
{
    internal class CreatePessoaHandler : IRequestHandler<CreatePessoaCommand, CreatePessoaResponse>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ILogger<CreatePessoaHandler> _logger;

        public CreatePessoaHandler(IPessoaRepository pessoaRepository, ILogger<CreatePessoaHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }


        public async Task<CreatePessoaResponse> Handle(CreatePessoaCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreatePessoaCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreatePessoaCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var nome = await _pessoaRepository.GetByName(command.Nome);

                    if (nome == null)
                    {
                        await _pessoaRepository.Add(command.GetEntity());
                        return new CreatePessoaResponse(command.Id, validationResult);
                    }

                    return new CreatePessoaResponse(command.Id, "Pessoa already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating extract");
                    return new CreatePessoaResponse(command.Id, "Error creating Adress");
                }
            }
            return new CreatePessoaResponse(command.Id, validationResult);
        }
    }
}
