using CloudSuite.Modules.Application.Handlers.Address.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Address.Requests
{
    public class CheckAddressExistsByAddressLineRequest : IRequest<CheckAddressExistsByAddressLineResponse>
    {

        public Guid Id { get; private set; }

        public string? AddressLine1 { get; set; }

        public CheckAddressExistsByAddressLineRequest(string? addressLine1)
        {
            Id = Guid.NewGuid();
            AddressLine1 = addressLine1;
        }
    }
}
