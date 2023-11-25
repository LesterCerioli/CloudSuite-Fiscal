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
    public class CheckNoteExistsByEmissionDateHandler : IRequestHandler<CheckNoteExistsByEmissionDateRequest, CheckNoteExistsByEmissionDateResponse>
    {
        private readonly INoteRepository _noteRepository;
        private readonly ILogger<CheckNoteExistsByEmissionDateHandler> _logger;

        public CheckNoteExistsByEmissionDateHandler(INoteRepository noteRepository, ILogger<CheckNoteExistsByEmissionDateHandler> logger)
        {
            _noteRepository = noteRepository;
            _logger = logger;
        }

        public async Task<CheckNoteExistsByEmissionDateResponse> Handle(CheckNoteExistsByEmissionDateRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckNoteExistsByEmissionDateRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckNoteExistsByEmissionDateRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var emissionDate = await _noteRepository.GetByEmissionDate(request.EmissionDate);

                    if (emissionDate != null)
                    {
                        return await Task.FromResult(new CheckNoteExistsByEmissionDateResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckNoteExistsByEmissionDateResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckNoteExistsByEmissionDateResponse(request.Id, false, validationResult));

        }
    }
}
