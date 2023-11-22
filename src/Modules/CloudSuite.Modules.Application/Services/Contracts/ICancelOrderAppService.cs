using CloudSuite.Modules.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Contracts
{
    public interface ICancelOrderAppService
    {
        Task<CancelOrderViewModel> GetbyCnpj(Cnpj cnpj);

        Task<CancelOrderViewModel> GetByRequestDate(DateTimeOffset requestDate);

        Task Save(CreateCancelOrderCommand commandCreate);
    }
}
