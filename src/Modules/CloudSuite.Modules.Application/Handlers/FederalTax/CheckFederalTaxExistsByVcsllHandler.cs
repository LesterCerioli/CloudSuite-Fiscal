using CloudSuite.Modules.Application.Handlers.DeclaracaoIR;
using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using CloudSuite.Modules.Application.Handlers.FederalTax.Requests;
using CloudSuite.Modules.Application.Handlers.FederalTax.Responses;
using CloudSuite.Modules.Application.Validations.DeclaracaoIR;
using CloudSuite.Modules.Application.Validations.FederalTax;
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

namespace CloudSuite.Modules.Application.Handlers.FederalTax
{
    public class CheckFederalTaxExistsByVcsllHandler : IRequestHandler<CheckFederalTaxExistsByVcsllRequest, CheckFederalTaxExistsByVcsllResponse>
    {
        private readonly IFederalTaxRepository _federalTaxRepository;
        private readonly ILogger<CheckFederalTaxExistsByVcsllHandler> _logger;

        public CheckFederalTaxExistsByVcsllHandler(IFederalTaxRepository federalTaxRepository, ILogger<CheckFederalTaxExistsByVcsllHandler> logger)
        {
            _federalTaxRepository = federalTaxRepository;
            _logger = logger;
        }
        public async Task<CheckFederalTaxExistsByVcsllResponse> Handle(CheckFederalTaxExistsByVcsllRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckFederalTaxExistsByVcsllRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckFederalTaxExistsByVcsllRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var alimony = await _federalTaxRepository.GetByVCSLL(request.VCSLL);

                    if (alimony != null)
                    {
                        return await Task.FromResult(new CheckFederalTaxExistsByVcsllResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckFederalTaxExistsByVcsllResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckFederalTaxExistsByVcsllResponse(request.Id, false, validationResult));

        }
    }
}
