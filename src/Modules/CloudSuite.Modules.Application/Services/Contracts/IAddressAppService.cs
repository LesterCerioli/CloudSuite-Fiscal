﻿using CloudSuite.Modules.Application.Handlers.Address;
using CloudSuite.Modules.Application.ViewModels;
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
        Task<AddressViewModel> GetByAddressLine1(string addressLine1);

        Task Save(CreateAddressCommand commandCreate);
    }
}
