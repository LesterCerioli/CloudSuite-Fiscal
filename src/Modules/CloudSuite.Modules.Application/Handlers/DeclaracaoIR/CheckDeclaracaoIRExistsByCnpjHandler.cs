using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Requests;
using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using CloudSuite.Modules.Application.Validations.DAS;
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
    public class CheckDeclaracaoIRExistsByCnpjHandler : IRequestHandler<CheckDeclaracaoIRExistsByCnpjRequest, CheckDeclaracaoIRExistsByCnpjResponse>
    {

        private readonly IDeclaracaoIRRepository _declaracaoIRRepository;
        private readonly ILogger<CheckDeclaracaoIRExistsByCnpjHandler> _logger;

        public CheckDeclaracaoIRExistsByCnpjHandler(IDeclaracaoIRRepository declaracaoIRRepository, ILogger<CheckDeclaracaoIRExistsByCnpjHandler> logger)
        {
            _declaracaoIRRepository = declaracaoIRRepository;
            _logger = logger;
        }
        public async Task<CheckDeclaracaoIRExistsByCnpjResponse> Handle(CheckDeclaracaoIRExistsByCnpjRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckDeclaracaoIRExistsByCnpjRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckDeclaracaoIRExistsByCnpjRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var cnpj = await _declaracaoIRRepository.GetByCnpj(request.Cnpj);

                    if (cnpj != null)
                    {
                        return await Task.FromResult(new CheckDeclaracaoIRExistsByCnpjResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckDeclaracaoIRExistsByCnpjResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckDeclaracaoIRExistsByCnpjResponse(request.Id, false, validationResult));

        }
    }
}
