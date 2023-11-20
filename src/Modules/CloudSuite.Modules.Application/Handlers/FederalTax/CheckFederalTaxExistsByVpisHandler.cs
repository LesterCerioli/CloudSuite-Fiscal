using CloudSuite.Modules.Application.Handlers.DeclaracaoIR;
using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using CloudSuite.Modules.Application.Handlers.FederalTax.Requests;
using CloudSuite.Modules.Application.Handlers.FederalTax.Responses;
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
    public class CheckFederalTaxExistsByVpisHandler : IRequestHandler<CheckFederalTaxExistsByVpisRequest, CheckFederalTaxExistsByVpisResponse>
    {
        private readonly IFederalTaxRepository _federalTaxRepository;
        private readonly ILogger<CheckFederalTaxExistsByVpisHandler> _logger;

        public CheckFederalTaxExistsByVpisHandler(IFederalTaxRepository federalTaxRepository, ILogger<CheckFederalTaxExistsByVpisHandler> logger)
        {
            _federalTaxRepository = federalTaxRepository;
            _logger = logger;
        }
        public async Task<CheckFederalTaxExistsByVpisResponse> Handle(CheckFederalTaxExistsByVpisRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckFederalTaxExistsByVpisRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckFederalTaxExistsByVpisRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var alimony = await _federalTaxRepository.GetByVPIS(request.VPIS);

                    if (alimony != null)
                    {
                        return await Task.FromResult(new CheckFederalTaxExistsByVpisResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckFederalTaxExistsByVpisResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckFederalTaxExistsByVpisResponse(request.Id, false, validationResult));

        }
    }
}
