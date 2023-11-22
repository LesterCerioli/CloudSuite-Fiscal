using CloudSuite.Modules.Application.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Implementation
{
    public class AddressAppService : IAddressAppService
    {
        public Task<AddressViewModels> GetByAddressLine(string addressLine1)
        {
            throw new NotImplementedException();
        }

        public Task Save(CreateAddressCommand commandCreate)
        {
            throw new NotImplementedException();
        }
    }
}
