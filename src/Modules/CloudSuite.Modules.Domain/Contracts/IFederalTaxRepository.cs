using CloudSuite.Modules.Domain.Models;

namespace CloudSuite.Modules.Domain.Contracts
{
    public interface IFederalTaxRepository
    {
        Task<FederalTax> GetByVPIS(decimal vPIS);

        Task<FederalTax> GetByVCOFINS(decimal vCOFINS);


        Task<FederalTax> GetByVIR(decimal vIR);

        Task<FederalTax> GetByVINSS(decimal vINSS);

        Task<FederalTax> GetByVCSLL(decimal vCSLL);

        Task<IEnumerable<FederalTax>> GetList();

        Task Add(FederalTax federalTax);

        void Update(FederalTax federalTax);

        void Remove(FederalTax federalTax);
        
        
         
    }
}