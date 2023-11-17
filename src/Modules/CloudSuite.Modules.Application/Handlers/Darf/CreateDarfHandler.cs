using CloudSuite.Modules.Application.Handlers.Darf.Responses;
using CloudSuite.Modules.Application.Validations.Darf;
using CloudSuite.Modules.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CloudSuite.Modules.Application.Handlers.Darf
{
    public class CreateDarfHandler : IRequestHandler<CreateDarfCommand, CreateDarfResponse>
    {
        private readonly IDarfRepository _darfRepository;
        private readonly ILogger<CreateDarfHandler> _logger;

        public CreateDarfHandler(IDarfRepository darfRepository, ILogger<CreateDarfHandler> logger)
        {
            _darfRepository = darfRepository;
            _logger = logger;
        }

        public async Task<CreateDarfResponse> Handle(CreateDarfCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateDarfCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateDarfCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var darfReferenceMonth = await _darfRepository.GetByReferenceMonth(command.ReferenceMonth);
                    var darfDueDate = await _darfRepository.GetByDueDate(command.DueDate);
                    var darfDocumentNumber = await _darfRepository.GetByDocumentNumber(command.DocumentNumber);
                    var darfValidationDate = await _darfRepository.GetByValidationDate(command.ValidationDate);


                    if (darfReferenceMonth == null && darfDueDate == null && darfDocumentNumber == null && darfValidationDate == null)
                    {
                        await _darfRepository.Add(command.GetEntity());
                        return new CreateDarfResponse(command.Id, validationResult);
                    }

                    return new CreateDarfResponse(command.Id, "Address already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating extract");
                    return new CreateDarfResponse(command.Id, "Error creating Adress");
                }
            }
            return new CreateDarfResponse(command.Id, validationResult);
        }
    }
}
