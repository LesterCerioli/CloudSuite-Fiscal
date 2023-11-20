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
    public class CheckDeclaracaoIRExistsByCpfHandler : IRequestHandler<CheckDeclaracaoIRExistsByCpfRequest, CheckDeclaracaoIRExistsByCpfResponse>
    {
        private readonly IDeclaracaoIRRepository _declaracaoIRRepository;
        private readonly ILogger<CheckDeclaracaoIRExistsByCpfHandler> _logger;

        public CheckDeclaracaoIRExistsByCpfHandler(IDeclaracaoIRRepository declaracaoIRRepository, ILogger<CheckDeclaracaoIRExistsByCpfHandler> logger)
        {
            _declaracaoIRRepository = declaracaoIRRepository;
            _logger = logger;
        }
        public async Task<CheckDeclaracaoIRExistsByCpfResponse> Handle(CheckDeclaracaoIRExistsByCpfRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckDeclaracaoIRExistsByCpfRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckDeclaracaoIRExistsByCpfRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var cnpj = await _declaracaoIRRepository.GetByCpf(request.Cpf);

                    if (cnpj != null)
                    {
                        return await Task.FromResult(new CheckDeclaracaoIRExistsByCpfResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckDeclaracaoIRExistsByCpfResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckDeclaracaoIRExistsByCpfResponse(request.Id, false, validationResult));

        }
    }
}
