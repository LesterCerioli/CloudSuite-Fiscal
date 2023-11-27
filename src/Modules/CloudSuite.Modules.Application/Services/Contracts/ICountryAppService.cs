using CloudSuite.Modules.Application.Handlers.Country;
using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Contracts
{
    public interface ICountryAppService
    {
        Task<CountryViewModel> GetbyCountryName(string countryName);

        Task Save(CreateCountryCommand commandCreate);
    }
}
