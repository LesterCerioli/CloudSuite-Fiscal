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
    public class CheckDeclaracaoIRExistsByTotalIncomeHandler : IRequestHandler<CheckDeclaracaoIRExistsByTotalIncomeRequest, CheckDeclaracaoIRExistsByTotalIncomeResponse>
    {
        private readonly IDeclaracaoIRRepository _declaracaoIRRepository;
        private readonly ILogger<CheckDeclaracaoIRExistsByTotalIncomeHandler> _logger;

        public CheckDeclaracaoIRExistsByTotalIncomeHandler(IDeclaracaoIRRepository declaracaoIRRepository, ILogger<CheckDeclaracaoIRExistsByTotalIncomeHandler> logger)
        {
            _declaracaoIRRepository = declaracaoIRRepository;
            _logger = logger;
        }

        public async Task<CheckDeclaracaoIRExistsByTotalIncomeResponse> Handle(CheckDeclaracaoIRExistsByTotalIncomeRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckDeclaracaoIRExistsByTotalIncomeRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckDeclaracaoIRExistsByTotalIncomeRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var totalIncome = await _declaracaoIRRepository.GetByTotalIncome(request.TotalIncome);

                    if (totalIncome != null)
                    {
                        return await Task.FromResult(new CheckDeclaracaoIRExistsByTotalIncomeResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckDeclaracaoIRExistsByTotalIncomeResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckDeclaracaoIRExistsByTotalIncomeResponse(request.Id, false, validationResult));

        }
    }
}
