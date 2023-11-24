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
    public class CheckDeclaracaoIRExistsByProfitsDividendsHandler : IRequestHandler<CheckDeclaracaoIRExistsByProfitsDividendsRequest, CheckDeclaracaoIRExistsByProfitsDividendsResponse>
    {
        private readonly IDeclaracaoIRRepository _declaracaoIRRepository;
        private readonly ILogger<CheckDeclaracaoIRExistsByProfitsDividendsHandler> _logger;

        public CheckDeclaracaoIRExistsByProfitsDividendsHandler(IDeclaracaoIRRepository declaracaoIRRepository, ILogger<CheckDeclaracaoIRExistsByProfitsDividendsHandler> logger)
        {
            _declaracaoIRRepository = declaracaoIRRepository;
            _logger = logger;
        }
        public async Task<CheckDeclaracaoIRExistsByProfitsDividendsResponse> Handle(CheckDeclaracaoIRExistsByProfitsDividendsRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckDeclaracaoIRExistsByProfitsDividendsRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckDeclaracaoIRExistsByProfitsDividendsRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var cnpj = await _declaracaoIRRepository.GetByProfitsDividends(request.ProfitsDividends);

                    if (cnpj != null)
                    {
                        return await Task.FromResult(new CheckDeclaracaoIRExistsByProfitsDividendsResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckDeclaracaoIRExistsByProfitsDividendsResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckDeclaracaoIRExistsByProfitsDividendsResponse(request.Id, false, validationResult));

        }
    }
}
