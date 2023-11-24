using CloudSuite.Modules.Application.Handlers.DeclaracaoIR;
using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using CloudSuite.Modules.Application.Handlers.Prestador.Requests;
using CloudSuite.Modules.Application.Handlers.Prestador.Responses;
using CloudSuite.Modules.Application.Validations.DeclaracaoIR;
using CloudSuite.Modules.Application.Validations.Prestador;
using CloudSuite.Modules.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Prestador
{
    public class CheckPrestadorExistsByInscricaoMunicipalHandler : IRequestHandler<CheckPrestadorExistsByInscricaoMunicipalRequest, CheckPrestadorExistsByInscricaoMunicipalResponse>
    {
        private readonly IPrestadorRepository _prestadorRepository;
        private readonly ILogger<CheckPrestadorExistsByInscricaoMunicipalHandler> _logger;

        public CheckPrestadorExistsByInscricaoMunicipalHandler(IPrestadorRepository prestadorRepository, ILogger<CheckPrestadorExistsByInscricaoMunicipalHandler> logger)
        {
            _prestadorRepository = prestadorRepository;
            _logger = logger;
        }
        public async Task<CheckPrestadorExistsByInscricaoMunicipalResponse> Handle(CheckPrestadorExistsByInscricaoMunicipalRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckPrestadorExistsByInscricaoMunicipalRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckPrestadorExistsByInscricaoMunicipalRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var inscricaoMunicipal = await _prestadorRepository.GetByInscricaoMunicipal(request.InscricaoMunicipal);

                    if (inscricaoMunicipal != null)
                    {
                        return await Task.FromResult(new CheckPrestadorExistsByInscricaoMunicipalResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckPrestadorExistsByInscricaoMunicipalResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckPrestadorExistsByInscricaoMunicipalResponse(request.Id, false, validationResult));

        }
    }
}
