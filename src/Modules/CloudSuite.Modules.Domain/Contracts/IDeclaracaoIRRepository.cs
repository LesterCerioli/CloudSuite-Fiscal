using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Models;

namespace CloudSuite.Modules.Domain.Contracts
{
    public interface IDeclaracaoIRRepository
    {
        Task<DeclaracaoIR> GetByDeclaracoaNumero(string declaracoaNumero);

        Task<DeclaracaoIR> GetByCnpj(Cnpj cnpj);

        Task<DeclaracaoIR> GetByCpf(Cpf cpf);

        Task<DeclaracaoIR> GetByTotalIncome(decimal totalIncome);

        Task<DeclaracaoIR> GetByAlimony(decimal alimony);

        Task<DeclaracaoIR> GetByPaidValueToBusiness(decimal paidValueToBusiness);

        Task<DeclaracaoIR> GetByProfitsDividends(decimal profitsDividends);

        Task Add(DeclaracaoIR declaracaoIR);

        void Update(DeclaracaoIR declaracaoIR);

        void Remove(DeclaracaoIR declaracaoIR);


         
    }
}