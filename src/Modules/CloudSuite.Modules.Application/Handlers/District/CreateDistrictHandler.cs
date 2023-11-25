using CloudSuite.Modules.Application.Handlers.DAS.Responses;
using CloudSuite.Modules.Application.Handlers.District.Responses;
using CloudSuite.Modules.Application.Validations.DeclaracaoIR;
using CloudSuite.Modules.Application.Validations.District;
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

namespace CloudSuite.Modules.Application.Handlers.District
{
    public class CreateDistrictHandler : IRequestHandler<CreateDistrictCommand, CreateDistrictResponse>
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly ILogger<CreateDistrictHandler> _logger;

        public CreateDistrictHandler(IDistrictRepository districtRepository, ILogger<CreateDistrictHandler> logger)
        {
            _districtRepository = districtRepository;
            _logger = logger;
        }

        public async Task<CreateDistrictResponse> Handle(CreateDistrictCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateDistrictCommand: {JsonSerializer.Serialize(command)}");
            var validationResult = new CreateDistrictCommandValidation().Validate(command);

            if (validationResult.IsValid)
            {
                try
                {
                    var cityName = await _districtRepository.GetByName(command.Name);


                    if (cityName == null)
                    {
                        await _districtRepository.Add(command.GetEntity());
                        return new CreateDistrictResponse(command.Id, validationResult);
                    }

                    return new CreateDistrictResponse(command.Id, "District already registered");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating District");
                    return new CreateDistrictResponse(command.Id, "Error creating District");
                }
            }
            return new CreateDistrictResponse(command.Id, validationResult);
        }
    }
}
