using CloudSuite.Modules.Domain.Models;

namespace CloudSuite.Modules.Domain.Contracts
{
    public interface IIdeCancelamentoRepository
    {
        Task<IdeCancelamento> GetByCancelReason(string cancelReason);

        Task<IdeCancelamento> GetByTimeDate(DateTimeOffset timeDate);
        
        Task<IEnumerable<IdeCancelamento>> GetList();

        Task Add(IdeCancelamento ideCancelamento);

        void Update(IdeCancelamento ideCancelamento);

        void Remove(IdeCancelamento ideCancelamento);
        
         
    }
}