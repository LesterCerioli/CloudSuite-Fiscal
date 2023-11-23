using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Contracts
{
    public interface ITomadorServicoAppService
    {
        Task<TomadorServicoViewModel> GetByCnpj(Cnpj cnpj);

        Task<TomadorServicoViewModel> GetBySocialReason(string socialReason);

        Task<TomadorServicoViewModel> Save(CreateTomadorServicoCommand createCommand);
    }
}
