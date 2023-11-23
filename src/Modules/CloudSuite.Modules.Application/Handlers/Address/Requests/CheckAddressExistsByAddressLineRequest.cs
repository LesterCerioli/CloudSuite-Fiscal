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

        public Guid Id { get; set; }

        public string? AddressLine1 { get; private set; }

        public CheckAddressExistsByAddressLineRequest(Guid id, string? addressLine1)
        {
            Id = Guid.NewGuid();
            AddressLine1 = addressLine1;
        }
    }
}
