using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Contracts
{
    public interface IAddressAppService
    {
        Task<AddressViewModels> GetByAddressLine(string addressLine1);

        Task Save(CreateAddressCommand commandCreate);
    }
}
