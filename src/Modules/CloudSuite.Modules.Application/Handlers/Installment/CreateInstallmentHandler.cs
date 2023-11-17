using CloudSuite.Modules.Application.Handlers.District.Responses;
using CloudSuite.Modules.Application.Handlers.IdeNFSe;
using CloudSuite.Modules.Application.Handlers.Installment.Responses;
using CloudSuite.Modules.Application.Validations.DeclaracaoIR;
using CloudSuite.Modules.Application.Validations.Installment;
using MediatR;
using Microsoft.Extensions.Logging;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Installment
{
    public class CreateInstallmentHandler : IRequestHandler<CreateInstallmentCommand, CreateInstallmentResponse>
    {
        private readonly InstallmentRepository _installmentRepository;
        private readonly ILogger<CreateInstallmentHandler> _logger;

        public CreateInstallmentHandler(InstallmentRepository installmentRepository, ILogger<CreateInstallmentHandler> logger)
        {
            _installmentRepository = installmentRepository;
            _logger = logger;
        }
        public async Task<CreateInstallmentResponse> Handle(CreateInstallmentCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateDistrictCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateInstallmentCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var DASReferenceMonth = await _installmentRepository.GetByReferenceMonth(command.ReferenceMonth);
                    var DASDueDate = await _installmentRepository.GetByDueDate(command.DueDate);
                    var DASDocumentNumber = await _installmentRepository.GetByDocumentNumber(command.DocumentNumber);
                    var DASReferenceYear = await _installmentRepository.GetByReferenceYear(command.ReferenceYear);


                    if (DASReferenceMonth == null && DASDueDate == null && DASDocumentNumber == null && DASReferenceYear == null)
                    {
                        await _installmentRepository.Add(command.GetEntity());
                        return new CreateInstallmentResponse(command.Id, validationResult);
                    }

                    return new CreateInstallmentResponse(command.Id, "Address already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating extract");
                    return new CreateInstallmentResponse(command.Id, "Error creating Adress");
                }
            }
            return new CreateDistrictResponse(command.Id, validationResult);
        }
    }
}
