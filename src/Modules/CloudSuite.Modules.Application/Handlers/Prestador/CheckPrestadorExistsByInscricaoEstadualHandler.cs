using CloudSuite.Modules.Application.Handlers.Prestador.Requests;
using CloudSuite.Modules.Application.Handlers.Prestador.Responses;
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
    public class CheckPrestadorExistsByInscricaoEstadualHandler : IRequestHandler<CheckPrestadorExistsByInscricaoEstadualRequest, CheckPrestadorExistsByInscricaoEstadualResponse>
    {
        private readonly IPrestadorRepository _prestadorRepository;
        private readonly ILogger<CheckPrestadorExistsByCnpjHandler> _logger;

        public CheckPrestadorExistsByInscricaoEstadualHandler(IPrestadorRepository prestadorRepository, ILogger<CheckPrestadorExistsByCnpjHandler> logger)
        {
            _prestadorRepository = prestadorRepository;
            _logger = logger;
        }

        public async Task<CheckPrestadorExistsByInscricaoEstadualResponse> Handle(CheckPrestadorExistsByInscricaoEstadualRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckPrestadorExistsByInscricaoEstadualRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckPrestadorExistsByInscricaoEstadualRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var cnpj = await _prestadorRepository.GetByInscricaoEstadual(request.InscricaoEstadual);

                    if (cnpj != null)
                    {
                        return await Task.FromResult(new CheckPrestadorExistsByInscricaoEstadualResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckPrestadorExistsByInscricaoEstadualResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckPrestadorExistsByInscricaoEstadualResponse(request.Id, false, validationResult));

        }
    }
}
