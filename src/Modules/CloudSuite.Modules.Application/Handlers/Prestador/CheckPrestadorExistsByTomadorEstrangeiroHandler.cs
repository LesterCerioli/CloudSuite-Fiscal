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
    public class CheckPrestadorExistsByTomadorEstrangeiroHandler : IRequestHandler<CheckPrestadorExistsByTomadorEstrangeiroRequest, CheckPrestadorExistsByTomadorEstrangeiroResponse>
    {
        private readonly IPrestadorRepository _prestadorRepository;
        private readonly ILogger<CheckPrestadorExistsByCnpjHandler> _logger;

        public CheckPrestadorExistsByTomadorEstrangeiroHandler(IPrestadorRepository prestadorRepository, ILogger<CheckPrestadorExistsByCnpjHandler> logger)
        {
            _prestadorRepository = prestadorRepository;
            _logger = logger;
        }
        public async Task<CheckPrestadorExistsByTomadorEstrangeiroResponse> Handle(CheckPrestadorExistsByTomadorEstrangeiroRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckPrestadorExistsByTomadorEstrangeiroRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckPrestadorExistsByTomadorEstrangeiroRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var tomadorEstrangeiro = await _prestadorRepository.GetByDocTomadorEstrangeiro(request.DocTomadorEstrangeiro);
                    
                    if (tomadorEstrangeiro != null)
                    {
                        return await Task.FromResult(new CheckPrestadorExistsByTomadorEstrangeiroResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckPrestadorExistsByTomadorEstrangeiroResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckPrestadorExistsByTomadorEstrangeiroResponse(request.Id, false, validationResult));

        }
    }
}
