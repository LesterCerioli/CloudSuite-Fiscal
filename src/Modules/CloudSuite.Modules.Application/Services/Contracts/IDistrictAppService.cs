using CloudSuite.Modules.Application.Handlers.District;
using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Contracts
{
    public interface IDistrictAppService
    {
        Task<DistrictViewModel> GetByName(string name);

        Task Save(CreateDistrictCommand createCommand);
    }
}
