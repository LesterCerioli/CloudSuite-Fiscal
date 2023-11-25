using CloudSuite.Modules.Application.Handlers.FederalTax;
using CloudSuite.Modules.Application.Handlers.FederalTax.Responses;
using CloudSuite.Modules.Application.Handlers.Note.Requests;
using CloudSuite.Modules.Application.Handlers.Note.Responses;
using CloudSuite.Modules.Application.Validations.FederalTax;
using CloudSuite.Modules.Application.Validations.Note;
using CloudSuite.Modules.Domain.Contracts;
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
    public class CheckNoteExistsByNoteNumberHandler : IRequestHandler<CheckNoteExistsByNoteNumberRequest, CheckNoteExistsByNoteNumberResponse>
    {
        private readonly INoteRepository _noteRepository;
        private readonly ILogger<CheckNoteExistsByNoteNumberHandler> _logger;

        public CheckNoteExistsByNoteNumberHandler(INoteRepository noteRepository, ILogger<CheckNoteExistsByNoteNumberHandler> logger)
        {
            _noteRepository = noteRepository;
            _logger = logger;
        }

        public async Task<CheckNoteExistsByNoteNumberResponse> Handle(CheckNoteExistsByNoteNumberRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckNoteExistsByNoteNumberRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckNoteExistsByNoteNumberRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var alimony = await _noteRepository.GetByNoteNumber(request.NoteNumber);

                    if (alimony != null)
                    {
                        return await Task.FromResult(new CheckNoteExistsByNoteNumberResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckNoteExistsByNoteNumberResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckNoteExistsByNoteNumberResponse(request.Id, false, validationResult));

        }
    }
}
