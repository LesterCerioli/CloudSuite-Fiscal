using CloudSuite.Modules.Application.Handlers.DeclaracaoIR;
using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using CloudSuite.Modules.Application.Handlers.FederalTax.Requests;
using CloudSuite.Modules.Application.Handlers.FederalTax.Responses;
using CloudSuite.Modules.Application.Validations.DeclaracaoIR;
using CloudSuite.Modules.Application.Validations.FederalTax;
using CloudSuite.Modules.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.FederalTax
{
    public class CheckFederalTaxExistsByVirHandler : IRequestHandler<CheckFederalTaxExistsByVirRequest, CheckFederalTaxExistsByVirResponse>
    {
        private readonly IFederalTaxRepository _federalTaxRepository;
        private readonly ILogger<CheckFederalTaxExistsByVirHandler> _logger;

        public CheckFederalTaxExistsByVirHandler(IFederalTaxRepository federalTaxRepository, ILogger<CheckFederalTaxExistsByVirHandler> logger)
        {
            _federalTaxRepository = federalTaxRepository;
            _logger = logger;
        }
        public async Task<CheckFederalTaxExistsByVirResponse> Handle(CheckFederalTaxExistsByVirRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckFederalTaxExistsByVirRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckFederalTaxExistsByVirRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var alimony = await _federalTaxRepository.GetByVIR(request.VIR);

                    if (alimony != null)
                    {
                        return await Task.FromResult(new CheckFederalTaxExistsByVirResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckFederalTaxExistsByVirResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckFederalTaxExistsByVirResponse(request.Id, false, validationResult));

        }
    }
}
