using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Contracts
{
    public interface IFederalTaxAppService
    {
        Task<FederalTaxViewModel> GetByVPIS(decimal vPIS);

        Task<FederalTaxViewModel> GetByVCOFINS(decimal vCOFINS);

        Task<FederalTaxViewModel> GetByVIR(decimal vIR);

        Task<FederalTaxViewModel> GetByVINSS(decimal vINSS);

        Task<FederalTaxViewModel> GetByVCSLL(decimal vCSLL);

        Task<FederalTaxViewModel> Saver(CreateFederalTaxCommand createCommand);
    }
}
