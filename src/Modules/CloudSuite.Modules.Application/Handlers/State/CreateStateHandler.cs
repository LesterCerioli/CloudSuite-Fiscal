using CloudSuite.Modules.Application.Handlers.Prestador;
using CloudSuite.Modules.Application.Handlers.Prestador.Responses;
using CloudSuite.Modules.Application.Handlers.State.Responses;
using CloudSuite.Modules.Application.Validations.Prestador;
using CloudSuite.Modules.Application.Validations.State;
using CloudSuite.Modules.Domain.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.State
{
    public class CreateStateHandler : IRequestHandler<CreateStateCommand, CreateStateResponse>
    {
        private readonly IStateRepository _stateRepository;
        private readonly ILogger<CreateStateHandler> _logger;

        public CreateStateHandler(IStateRepository stateRepository, ILogger<CreateStateHandler> logger)
        {
            _stateRepository = stateRepository;
            _logger = logger;
        }
        public async Task<CreateStateResponse> Handle(CreateStateCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreatePrestadorCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateStateCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var stateName = await _stateRepository.GetByStateName(command.StateName);
                    var Uf = await _stateRepository.GetByUF(command.UF);


                    if (stateName == null && Uf == null)
                    {
                        await _stateRepository.Add(command.GetEntity());
                        return new CreateStateResponse(command.Id, validationResult);
                    }

                    return new CreateStateResponse(command.Id, "State already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating State");
                    return new CreateStateResponse(command.Id, "Error creating Adress");
                }
            }
            return new CreateStateResponse(command.Id, validationResult);

        }
    }
}
