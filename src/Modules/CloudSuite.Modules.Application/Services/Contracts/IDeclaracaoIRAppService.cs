using CloudSuite.Modules.Application.Handlers.DeclaracaoIR;
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
    public interface IDeclaracaoIRAppService
    {
        Task<DeclaracaoIRViewModel> GetByDeclaracaoNumero(string declaracaoNumero);

        Task<DeclaracaoIRViewModel> GetByCnpj(Cnpj cnpj);

        Task<DeclaracaoIRViewModel> GetByCpf(Cpf cpf);

        Task<DeclaracaoIRViewModel> GetByTotalIncome(decimal totalIncome);

        Task<DeclaracaoIRViewModel> GetByAlimony(decimal alimony);

        Task<DeclaracaoIRViewModel> GetByPaidValueToBusiness(decimal paidValueToBusiness);

        Task<DeclaracaoIRViewModel> GetByProfitsDividends(decimal profitsDividends);

        Task Save(CreateDeclaracaoIRCommand createCommand);
    }
}
