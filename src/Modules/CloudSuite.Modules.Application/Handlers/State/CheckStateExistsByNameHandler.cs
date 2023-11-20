using CloudSuite.Modules.Application.Handlers.Prestador;
using CloudSuite.Modules.Application.Handlers.Prestador.Responses;
using CloudSuite.Modules.Application.Handlers.State.Requests;
using CloudSuite.Modules.Application.Handlers.State.Responses;
using CloudSuite.Modules.Application.Validations.Prestador;
using CloudSuite.Modules.Application.Validations.State;
using CloudSuite.Modules.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.State
{
    public class CheckStateExistsByNameHandler : IRequestHandler<CheckStateExistsByNameRequest, CheckStateExistsByNameResponse>
    {
        private readonly IStateRepository _stateRepository;
        private readonly ILogger<CheckStateExistsByNameHandler> _logger;

        public CheckStateExistsByNameHandler(IStateRepository stateRepository, ILogger<CheckStateExistsByNameHandler> logger)
        {
            _stateRepository = stateRepository;
            _logger = logger;
        }

        public async Task<CheckStateExistsByNameResponse> Handle(CheckStateExistsByNameRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckStateExistsByNameRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckStateExistsByNameRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var stateName = await _stateRepository.GetByStateName(request.StateName);

                    if (stateName != null)
                    {
                        return await Task.FromResult(new CheckStateExistsByNameResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckStateExistsByNameResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckStateExistsByNameResponse(request.Id, false, validationResult));

        }
    }
}
