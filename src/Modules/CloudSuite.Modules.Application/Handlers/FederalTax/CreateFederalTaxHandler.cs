using CloudSuite.Modules.Application.Handlers.District;
using CloudSuite.Modules.Application.Handlers.District.Responses;
using CloudSuite.Modules.Application.Handlers.FederalTax.Responses;
using CloudSuite.Modules.Application.Validations.DeclaracaoIR;
using CloudSuite.Modules.Application.Validations.FederalTax;
using CloudSuite.Modules.Domain.Contracts;
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
        private readonly IFederalTaxRepository _federalTaxRepository;
        private readonly ILogger<CreateFederalTaxHandler> _logger;

        public CreateFederalTaxHandler(IFederalTaxRepository federalTaxRepository, ILogger<CreateFederalTaxHandler> logger)
        {
            _federalTaxRepository = federalTaxRepository;
            _logger = logger;
        }
        public async Task<CreateFederalTaxResponse> Handle(CreateFederalTaxCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateFederalTaxCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateFederalTaxCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var vPis = await _federalTaxRepository.GetByVPIS(command.VPIS);
                    var vCofins = await _federalTaxRepository.GetByVCOFINS(command.VCOFINS);
                    var vir = await _federalTaxRepository.GetByVIR(command.VIR);
                    var vinss = await _federalTaxRepository.GetByVINSS(command.VINSS);
                    var vcsll = await _federalTaxRepository.GetByVCSLL(command.VCSLL);



                    if (vPis == null && vCofins == null && vir == null && vinss == null && vcsll == null)
                    {
                        await _federalTaxRepository.Add(command.GetEntity());
                        return new CreateFederalTaxResponse(command.Id, validationResult);
                    }

                    return new CreateFederalTaxResponse(command.Id, "Address already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating FederalTax");
                    return new CreateFederalTaxResponse(command.Id, "Error creating FederalTax");
                }
            }
            return new CreateFederalTaxResponse(command.Id, validationResult);
        }
    }
}
