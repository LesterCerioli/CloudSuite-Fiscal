using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Requests;
using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using CloudSuite.Modules.Application.Validations.DeclaracaoIR;
using CloudSuite.Modules.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.DeclaracaoIR
{
    public class CheckDeclaracaoIRExistsByPaidValueToBusinessHandler : IRequestHandler<CheckDeclaracaoIRExistsByPaidValueToBusinessRequest, CheckDeclaracaoIRExistsByPaidValueToBusinessResponse>
    {
        private readonly IDeclaracaoIRRepository _declaracaoIRRepository;
        private readonly ILogger<CheckDeclaracaoIRExistsByPaidValueToBusinessHandler> _logger;

        public CheckDeclaracaoIRExistsByPaidValueToBusinessHandler(IDeclaracaoIRRepository declaracaoIRRepository, ILogger<CheckDeclaracaoIRExistsByPaidValueToBusinessHandler> logger)
        {
            _declaracaoIRRepository = declaracaoIRRepository;
            _logger = logger;
        }

        public async Task<CheckDeclaracaoIRExistsByPaidValueToBusinessResponse> Handle(CheckDeclaracaoIRExistsByPaidValueToBusinessRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckDeclaracaoIRExistsByPaidValueToBusinessRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckDeclaracaoIRExistsByPaidValueToBusinessRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var paidValueToBussiness = await _declaracaoIRRepository.GetByPaidValueToBusiness(request.PaidValueToBusiness);

                    if (paidValueToBussiness != null)
                    {
                        return await Task.FromResult(new CheckDeclaracaoIRExistsByPaidValueToBusinessResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckDeclaracaoIRExistsByPaidValueToBusinessResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckDeclaracaoIRExistsByPaidValueToBusinessResponse(request.Id, false, validationResult));

        }
    }
}
