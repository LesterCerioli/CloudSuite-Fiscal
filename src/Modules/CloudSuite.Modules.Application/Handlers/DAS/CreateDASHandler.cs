using CloudSuite.Modules.Application.Handlers.Darf.Responses;
using CloudSuite.Modules.Application.Handlers.DAS.Responses;
using CloudSuite.Modules.Application.Validations.Darf;
using CloudSuite.Modules.Application.Validations.DAS;
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

namespace CloudSuite.Modules.Application.Handlers.DAS
{
    public class CreateDASHandler : IRequestHandler<CreateDASCommand, CreateDASResponse>
    {
        private readonly IDASRepository _dasRepository;
        private readonly ILogger<CreateDASHandler> _logger;

        public CreateDASHandler(IDASRepository dasRepository, ILogger<CreateDASHandler> logger)
        {
            _dasRepository = dasRepository;
            _logger = logger;
        }

        public async Task<CreateDASResponse> Handle(CreateDASCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateDASCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateDASCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var DASReferenceMonth = await _dasRepository.GetByReferenceMonth(command.ReferenceMonth);
                    var DASDueDate = await _dasRepository.GetByDueDate(command.DueDate);
                    var DASDocumentNumber = await _dasRepository.GetByDocumentNumber(command.DocumentNumber);
                    var DASReferenceYear = await _dasRepository.GetByReferenceYear(command.ReferenceYear);


                    if (DASReferenceMonth == null && DASDueDate == null && DASDocumentNumber == null && DASReferenceYear == null)
                    {
                        await _dasRepository.Add(command.GetEntity());
                        return new CreateDASResponse(command.Id, validationResult);
                    }

                    return new CreateDASResponse(command.Id, "Address already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating extract");
                    return new CreateDASResponse(command.Id, "Error creating Adress");
                }
            }
            return new CreateDASResponse(command.Id, validationResult);
        }
    }
}
