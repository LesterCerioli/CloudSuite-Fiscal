using CloudSuite.Modules.Application.Handler.Requests;
using CloudSuite.Modules.Application.Handler.Responses;
using CloudSuite.Modules.Application.Validation;
using CloudSuite.Modules.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handler
{
    internal class CheckPessoaExistsByNameHandler : IRequestHandler<CheckPessoaExistsByNameRequest, CheckPessoaExistsByNameResponse>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ILogger<CreatePessoaHandler> _logger;

        public CheckPessoaExistsByNameHandler(IPessoaRepository pessoaRepository, ILogger<CreatePessoaHandler> logger)
        {
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }

        public async  Task<CheckPessoaExistsByNameResponse> Handle(CheckPessoaExistsByNameRequest request, CancellationToken cancellationToken)
        {
           _logger.LogInformation($"CheckPessoaExistsByNameRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckPessoaExistsByNameRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var contactName = await _pessoaRepository.GetByName(request.Nome);

                    if (contactName != null)
                    {
                        return await Task.FromResult(new CheckPessoaExistsByNameResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckPessoaExistsByNameResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckPessoaExistsByNameResponse(request.Id, false, validationResult));

        }
    }
}
