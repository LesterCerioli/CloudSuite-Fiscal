using CloudSuite.Modules.Application.Handlers.Note.Requests;
using CloudSuite.Modules.Application.Handlers.Note.Responses;
using CloudSuite.Modules.Application.Validations.FederalTax;
using CloudSuite.Modules.Application.Validations.Note;
using CloudSuite.Modules.Domain.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Note
{
    public class CheckNoteExistsByCnpjHandler : IRequestHandler<CheckNoteExistsByCnpjRequest, CheckNoteExistsByCnpjResponse>
    {
        private readonly INoteRepository _noteRepository;
        private readonly ILogger<CheckNoteExistsByCnpjHandler> _logger;

        public CheckNoteExistsByCnpjHandler(INoteRepository noteRepository, ILogger<CheckNoteExistsByCnpjHandler> logger)
        {
            _noteRepository = noteRepository;
            _logger = logger;
        }

        public async Task<CheckNoteExistsByCnpjResponse> Handle(CheckNoteExistsByCnpjRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckFederalTaxExistsByVpisRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckNoteExistsByCnpjRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var alimony = await _noteRepository.GetByCnpj(request.Cnpj);

                    if (alimony != null)
                    {
                        return await Task.FromResult(new CheckNoteExistsByCnpjResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckNoteExistsByCnpjResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckNoteExistsByCnpjResponse(request.Id, false, validationResult));
        }
    }
}
