using CloudSuite.Modules.Application.Handlers.District.Responses;
using CloudSuite.Modules.Application.Handlers.Note.Responses;
using CloudSuite.Modules.Application.Validations.Note;
using CloudSuite.Modules.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Note
{
    public class CreateNoteHandler : IRequestHandler<CreateNoteCommand, CreateNoteResponse>
    {
        private readonly INoteRepository _noteRepository;
        private readonly ILogger<CreateNoteHandler> _logger;

        public CreateNoteHandler(INoteRepository noteRepository, ILogger<CreateNoteHandler> logger)
        {
            _noteRepository = noteRepository;
            _logger = logger;
        }

        public async Task<CreateNoteResponse> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateNoteCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateNoteCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var DASReferenceMonth = await _noteRepository.GetByCnpj(command.Cnpj);
                    var DASDueDate = await _noteRepository.GetByNoteNumber(command.NoteNumber);
                    var DASDocumentNumber = await _noteRepository.GetByValue(command.Value);
                    var DASReferenceYear = await _noteRepository.GetByEmissionDate(command.EmissionDate);


                    if (DASReferenceMonth == null && DASDueDate == null && DASDocumentNumber == null && DASReferenceYear == null)
                    {
                        await _noteRepository.Add(command.GetEntity());
                        return new CreateNoteResponse(command.Id, validationResult);
                    }
                    return new CreateNoteResponse(command.Id, "Note already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating Note");
                    return new CreateNoteResponse(command.Id, "Error creating Note");
                }
            }
            return new CreateNoteResponse(command.Id, validationResult);
        }
    }
}
