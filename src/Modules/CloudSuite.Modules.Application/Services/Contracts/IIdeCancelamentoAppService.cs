using CloudSuite.Modules.Application.Handlers.IdeCancelamento;
using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Contracts
{
    public interface IIdeCancelamentoAppService
    {
        Task<IdeCancelamentoViewModel> GetByCancelReason(string cancelReason);

        Task<IdeCancelamentoViewModel> GetByTimeDate(DateTimeOffset timeDate);

        Task Save(CreateIdeCancelamentoCommand createCommand);
    }
}
