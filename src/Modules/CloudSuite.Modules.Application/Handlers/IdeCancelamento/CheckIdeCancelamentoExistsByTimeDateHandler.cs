using CloudSuite.Modules.Application.Handlers.IdeCancelamento.Requests;
using CloudSuite.Modules.Application.Handlers.IdeCancelamento.Responses;
using CloudSuite.Modules.Application.Validations.IdeCancelamento;
using CloudSuite.Modules.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.IdeCancelamento
{
    public class CheckIdeCancelamentoExistsByTimeDateHandler : IRequestHandler<CheckIdeCancelamentoExistsByTimeDateRequest, CheckIdeCancelamentoExistsByTimeDateResponse>
    {
        private readonly IIdeCancelamentoRepository _ideCancelamentoRepository;
        private readonly ILogger<CheckIdeCancelamentoExistsByTimeDateHandler> _logger;

        public CheckIdeCancelamentoExistsByTimeDateHandler(IIdeCancelamentoRepository ideCancelamentoRepository, ILogger<CheckIdeCancelamentoExistsByTimeDateHandler> logger)
        {
            _ideCancelamentoRepository = ideCancelamentoRepository;
            _logger = logger;
        }
        public async Task<CheckIdeCancelamentoExistsByTimeDateResponse> Handle(CheckIdeCancelamentoExistsByTimeDateRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckIdeCancelamentoExistsByTimeDateRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckIdeCancelamentoExistsByTimeDateRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var timeDate = await _ideCancelamentoRepository.GetByTimeDate(request.TimeDate);

                    if (timeDate != null)
                    {
                        return await Task.FromResult(new CheckIdeCancelamentoExistsByTimeDateResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckIdeCancelamentoExistsByTimeDateResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckIdeCancelamentoExistsByTimeDateResponse(request.Id, false, validationResult));
        }
    }
}
