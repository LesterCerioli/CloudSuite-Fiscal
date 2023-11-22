using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Contracts
{
    public interface ICityAppService
    {
        Task<CityViewModel> GetByCityName(string cityName);

        Task Save(CreateCityCommand commandCreate);
    }
}
