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
    public class CheckPrestadorExistsByNomeFantasiaHandler : IRequestHandler<CheckPrestadorExistsByNomeFantasiaRequest, CheckPrestadorExistsByNomeFantasiaResponse>
    {
        private readonly IPrestadorRepository _prestadorRepository;
        private readonly ILogger<CheckPrestadorExistsByCnpjHandler> _logger;

        public CheckPrestadorExistsByNomeFantasiaHandler(IPrestadorRepository prestadorRepository, ILogger<CheckPrestadorExistsByCnpjHandler> logger)
        {
            _prestadorRepository = prestadorRepository;
            _logger = logger;
        }
        public async Task<CheckPrestadorExistsByNomeFantasiaResponse> Handle(CheckPrestadorExistsByNomeFantasiaRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckPrestadorExistsByNomeFantasiaRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckPrestadorExistsByNomeFantasiaRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var nomeFantasia = await _prestadorRepository.GetByNomeFantasia(request.NomeFantasia);

                    if (nomeFantasia != null)
                    {
                        return await Task.FromResult(new CheckPrestadorExistsByNomeFantasiaResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckPrestadorExistsByNomeFantasiaResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckPrestadorExistsByNomeFantasiaResponse(request.Id, false, validationResult));

        }
    }
}
