using CloudSuite.Modules.Application.Handlers.DAS.Responses;
using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using CloudSuite.Modules.Application.Validations.DAS;
using CloudSuite.Modules.Application.Validations.DeclaracaoIR;
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

namespace CloudSuite.Modules.Application.Handlers.DeclaracaoIR
{
    public class CreateDeclaracaoIRHandler : IRequestHandler<CreateDeclaracaoIRCommand, CreateDeclaracaoIRResponse>
    {
        private IDeclaracaoIRRepository _declaracaoIRRepository;
        private ILogger<CreateDeclaracaoIRHandler> _logger;

        public CreateDeclaracaoIRHandler(IDeclaracaoIRRepository declaracaoIRRepository, ILogger<CreateDeclaracaoIRHandler> logger)
        {
            _declaracaoIRRepository = declaracaoIRRepository;
            _logger = logger;
        }

        public async Task<CreateDeclaracaoIRResponse> Handle(CreateDeclaracaoIRCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateDASCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateDeclaracaoIRCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var declaracaoNum = await _declaracaoIRRepository.GetByDeclaracaoNumero(command.DeclaracoaNumero);
                    var cnpj = await _declaracaoIRRepository.GetByCnpj(command.Cnpj);
                    var cpf = await _declaracaoIRRepository.GetByCpf(command.Cpf);
                    var totalIncome = await _declaracaoIRRepository.GetByTotalIncome(command.TotalIncome);
                    var alimony = await _declaracaoIRRepository.GetByAlimony(command.Alimony);
                    var profitsDividends = await _declaracaoIRRepository.GetByProfitsDividends(command.ProfitsDividends);
                    var paidValue = await _declaracaoIRRepository.GetByPaidValueToBusiness(command.PaidValueToBusiness);


                    if (declaracaoNum == null && cnpj == null && cpf == null && totalIncome == null && alimony == null 
                        && profitsDividends == null && paidValue == null)
                    {
                        await _declaracaoIRRepository.Add(command.GetEntity());
                        return new CreateDeclaracaoIRResponse(command.Id, validationResult);
                    }

                    return new CreateDeclaracaoIRResponse(command.Id, "DeclaracaoIR already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating extract");
                    return new CreateDeclaracaoIRResponse(command.Id, "Error creating Adress");
                }
            }
            return new CreateDeclaracaoIRResponse(command.Id, validationResult);
        }
    }
}
