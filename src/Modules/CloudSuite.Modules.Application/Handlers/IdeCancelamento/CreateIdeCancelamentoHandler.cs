using CloudSuite.Modules.Application.Handlers.District;
using CloudSuite.Modules.Application.Handlers.District.Responses;
using CloudSuite.Modules.Application.Handlers.IdeCancelamento.Responses;
using CloudSuite.Modules.Application.Validations.DeclaracaoIR;
using CloudSuite.Modules.Application.Validations.IdeCancelamento;
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

namespace CloudSuite.Modules.Application.Handlers.IdeCancelamento
{
    public class CreateIdeCancelamentoHandler : IRequestHandler<CreateIdeCancelamentoCommand, CreateIdeCancelamentoResponse>
    {
        private readonly IdeCancelamentoRepository _ideCancelamentoRepository;
        private readonly ILogger<CreateIdeCancelamentoHandler> _logger;

        public CreateIdeCancelamentoHandler(IdeCancelamentoRepository ideCancelamentoRepository, ILogger<CreateIdeCancelamentoHandler> logger)
        {
            _ideCancelamentoRepository = ideCancelamentoRepository;
            _logger = logger;
        }
        public async Task<CreateIdeCancelamentoResponse> Handle(CreateIdeCancelamentoCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateIdeCancelamentoCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateIdeCancelamentoCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var IdCancelOrder = await _ideCancelamentoRepository.GetByCancelOrder(command.CancelOrder);
                    var IdCancelReason = await _ideCancelamentoRepository.GetByCancelReason(command.CancelReason);
                    var IdTimeDate = await _ideCancelamentoRepository.GetByTimeDate(command.TimeDate);


                    if (IdCancelOrder == null && IdCancelReason == null && IdTimeDate == null)
                    {
                        await _ideCancelamentoRepository.Add(command.GetEntity());
                        return new CreateIdeCancelamentoResponse(command.Id, validationResult);
                    }

                    return new CreateIdeCancelamentoResponse(command.Id, "IdeCancelamento already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating extract");
                    return new CreateIdeCancelamentoResponse(command.Id, "Error creating Adress");
                }
            }
            return new CreateIdeCancelamentoResponse(command.Id, validationResult);

        }
    }
}
