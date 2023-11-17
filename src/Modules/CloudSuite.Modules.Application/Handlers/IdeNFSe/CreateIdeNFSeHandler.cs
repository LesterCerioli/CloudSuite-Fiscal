using CloudSuite.Modules.Application.Handlers.District.Responses;
using CloudSuite.Modules.Application.Handlers.IdeNFSe.Responses;
using CloudSuite.Modules.Application.Validations.DeclaracaoIR;
using CloudSuite.Modules.Application.Validations.IdeNFSe;
using MediatR;
using Microsoft.Extensions.Logging;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.IdeNFSe
{
    public class CreateIdeNFSeHandler : IRequestHandler<CreateIdeNFSeCommand, CreateIdeNFSeResponse>
    {
        private readonly IIdeNFSeRepository _ideNFSeRepository;
        private readonly ILogger<CreateIdeNFSeHandler> _logger;

        public CreateIdeNFSeHandler(IIdeNFSeRepository ideNFSeRepository, ILogger<CreateIdeNFSeHandler> logger)
        {
            _ideNFSeRepository = ideNFSeRepository;
            _logger = logger;
        }

        public Task<CreateIdeNFSeResponse> Handle(CreateIdeNFSeCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateIdeNFSeCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateIdeNFSeCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var DASReferenceMonth = await _ideNFSeRepository.GetByReferenceMonth(command.ReferenceMonth);
                    var DASDueDate = await _ideNFSeRepository.GetByDueDate(command.DueDate);
                    var DASDocumentNumber = await _ideNFSeRepository.GetByDocumentNumber(command.DocumentNumber);
                    var DASReferenceYear = await _ideNFSeRepository.GetByReferenceYear(command.ReferenceYear);


                    if (DASReferenceMonth == null && DASDueDate == null && DASDocumentNumber == null && DASReferenceYear == null)
                    {
                        await _ideNFSeRepository.Add(command.GetEntity());
                        return new CreateIdeNFSeResponse(command.Id, validationResult);
                    }

                    return new CreateIdeNFSeResponse(command.Id, "Address already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating extract");
                    return new CreateIdeNFSeResponse(command.Id, "Error creating Adress");
                }
            }
            return new CreateIdeNFSeResponse(command.Id, validationResult);
        }
    }
}
