using CloudSuite.Modules.Application.Handlers.District.Responses;
using CloudSuite.Modules.Application.Handlers.Installment.Responses;
using CloudSuite.Modules.Application.Handlers.Note.Responses;
using CloudSuite.Modules.Application.Validations.Installment;
using CloudSuite.Modules.Application.Validations.Note;
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
                    var DASReferenceMonth = await _noteRepository.GetByReferenceMonth(command.ReferenceMonth);
                    var DASDueDate = await _noteRepository.GetByDueDate(command.DueDate);
                    var DASDocumentNumber = await _noteRepository.GetByDocumentNumber(command.DocumentNumber);
                    var DASReferenceYear = await _noteRepository.GetByReferenceYear(command.ReferenceYear);


                    if (DASReferenceMonth == null && DASDueDate == null && DASDocumentNumber == null && DASReferenceYear == null)
                    {
                        await _noteRepository.Add(command.GetEntity());
                        return new CreateNoteResponse(command.Id, validationResult);
                    }
                    return new CreateInstalCreateNoteResponselmentResponse(command.Id, "Address already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating extract");
                    return new CreateNoteResponse(command.Id, "Error creating Adress");
                }
            }
            return new CreateNoteResponse(command.Id, validationResult);
        }
    }
}
