using CloudSuite.Modules.Application.Handlers.Address.Requests;
using CloudSuite.Modules.Application.Handlers.Address.Responses;
using CloudSuite.Modules.Application.Handlers.FederalTax;
using CloudSuite.Modules.Application.Handlers.FederalTax.Responses;
using CloudSuite.Modules.Application.Validations.Address;
using CloudSuite.Modules.Application.Validations.FederalTax;
using CloudSuite.Modules.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Address
{
    public class CheckAddressExistsByAddressLineHandler : IRequestHandler<CheckAddressExistsByAddressLineRequest, CheckAddressExistsByAddressLineResponse>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<CheckAddressExistsByAddressLineHandler> _logger;

        public CheckAddressExistsByAddressLineHandler(IAddressRepository addressRepository, ILogger<CheckAddressExistsByAddressLineHandler> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }
        public async Task<CheckAddressExistsByAddressLineResponse> Handle(CheckAddressExistsByAddressLineRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckAddressExistsByAddressLineRequest: {JsonSerializer.Serialize(request)}");
            var validationResult = new CheckAddressExistsByAddressLineRequestValidation().Validate(request);

            if (validationResult.IsValid)
            {
                try
                {
                    var addressLine = await _addressRepository.GetByAddressLine1(request.AddressLine1);

                    if (addressLine != null)
                    {
                        return await Task.FromResult(new CheckAddressExistsByAddressLineResponse(request.Id, true, validationResult));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return await Task.FromResult(new CheckAddressExistsByAddressLineResponse(request.Id, "Failed to process the request."));
                }
            }
            return await Task.FromResult(new CheckAddressExistsByAddressLineResponse(request.Id, false, validationResult));

        }
    }
}
