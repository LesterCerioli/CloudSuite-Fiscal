using CloudSuite.Modules.Application.Handlers.DAS;
using CloudSuite.Modules.Application.Handlers.DAS.Responses;
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
    public class CheckDeclaracaoIRExistsByDeclaracaoNumeroHandler : IRequestHandler<CheckDeclaracaoIRExistsByDeclaracaoNumeroRequest, CheckDeclaracaoIRExistsByDeclaracaoNumeroResponse>
    {
        private readonly IDeclaracaoIRRepository _declaracaoIRRepository;
        private readonly ILogger<CheckDeclaracaoIRExistsByDeclaracaoNumeroHandler> _logger;

        public CheckDeclaracaoIRExistsByDeclaracaoNumeroHandler(IDeclaracaoIRRepository declaracaoIRRepository, ILogger<CheckDeclaracaoIRExistsByDeclaracaoNumeroHandler> logger)
        {
            _declaracaoIRRepository = declaracaoIRRepository;
            _logger = logger;
        }

        public async Task<CheckDeclaracaoIRExistsByDeclaracaoNumeroResponse> Handle(CheckDeclaracaoIRExistsByDeclaracaoNumeroRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckDeclaracaoIRExistsByDeclaracaoNumeroRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckDeclaracaoIRExistsByDeclaracaoNumeroRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var cityName = await _declaracaoIRRepository.GetByDeclaracoaNumero(request.DeclaracoaNumero);

                    if (cityName != null)
                    {
                        return await Task.FromResult(new CheckDeclaracaoIRExistsByDeclaracaoNumeroResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckDeclaracaoIRExistsByDeclaracaoNumeroResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckDeclaracaoIRExistsByDeclaracaoNumeroResponse(request.Id, false, validationResult));

        }
    }
}
