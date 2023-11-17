using CloudSuite.Modules.Application.Handlers.District;
using CloudSuite.Modules.Application.Handlers.District.Responses;
using CloudSuite.Modules.Application.Handlers.FederalTax.Responses;
using CloudSuite.Modules.Application.Validations.DeclaracaoIR;
using CloudSuite.Modules.Application.Validations.FederalTax;
using CloudSuite.Modules.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.FederalTax
{
    public class CreateFederalTaxHandler : IRequestHandler<CreateFederalTaxCommand, CreateFederalTaxResponse>
    {
        private readonly IFederalTax _federalTaxRepository;
        private readonly ILogger<CreateFederalTaxHandler> _logger;

        public CreateFederalTaxHandler(IFederalTax federalTaxRepository, ILogger<CreateFederalTaxHandler> logger)
        {
            _federalTaxRepository = federalTaxRepository;
            _logger = logger;
        }
        public Task<CreateFederalTaxResponse> Handle(CreateFederalTaxCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateFederalTaxCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateFederalTaxCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var DASReferenceMonth = await _federalTaxRepository.GetByReferenceMonth(command.ReferenceMonth);
                    var DASDueDate = await _federalTaxRepository.GetByDueDate(command.DueDate);
                    var DASDocumentNumber = await _federalTaxRepository.GetByDocumentNumber(command.DocumentNumber);
                    var DASReferenceYear = await _federalTaxRepository.GetByReferenceYear(command.ReferenceYear);


                    if (DASReferenceMonth == null && DASDueDate == null && DASDocumentNumber == null && DASReferenceYear == null)
                    {
                        await _federalTaxRepository.Add(command.GetEntity());
                        return new CreateFederalTaxResponse(command.Id, validationResult);
                    }

                    return new CreateFederalTaxResponse(command.Id, "Address already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating extract");
                    return new CreateFederalTaxResponse(command.Id, "Error creating Adress");
                }
            }
            return new CreateFederalTaxResponse(command.Id, validationResult);
        }
    }
}
