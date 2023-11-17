using CloudSuite.Modules.Application.Handlers.District;
using CloudSuite.Modules.Application.Handlers.District.Responses;
using CloudSuite.Modules.Application.Handlers.IdeCancelamento.Responses;
using CloudSuite.Modules.Application.Validations.DeclaracaoIR;
using CloudSuite.Modules.Application.Validations.IdeCancelamento;
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
        private readonly IIdeCancelamentoRepository _ideCancelamentoRepository;
        private readonly ILogger<CreateIdeCancelamentoHandler> _logger;

        public CreateIdeCancelamentoHandler(IIdeCancelamentoRepository ideCancelamentoRepository, ILogger<CreateIdeCancelamentoHandler> logger)
        {
            _ideCancelamentoRepository = ideCancelamentoRepository;
            _logger = logger;
        }
        public Task<CreateIdeCancelamentoResponse> Handle(CreateIdeCancelamentoCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateIdeCancelamentoCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateIdeCancelamentoCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var DASReferenceMonth = await _ideCancelamentoRepository.GetByReferenceMonth(command.ReferenceMonth);
                    var DASDueDate = await _ideCancelamentoRepository.GetByDueDate(command.DueDate);
                    var DASDocumentNumber = await _ideCancelamentoRepository.GetByDocumentNumber(command.DocumentNumber);
                    var DASReferenceYear = await _ideCancelamentoRepository.GetByReferenceYear(command.ReferenceYear);


                    if (DASReferenceMonth == null && DASDueDate == null && DASDocumentNumber == null && DASReferenceYear == null)
                    {
                        await _ideCancelamentoRepository.Add(command.GetEntity());
                        return new CreateDistrictResponse(command.Id, validationResult);
                    }

                    return new CreateDistrictResponse(command.Id, "Address already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating extract");
                    return new CreateDistrictResponse(command.Id, "Error creating Adress");
                }
            }
            return new CreateDistrictResponse(command.Id, validationResult);

        }
    }
}
