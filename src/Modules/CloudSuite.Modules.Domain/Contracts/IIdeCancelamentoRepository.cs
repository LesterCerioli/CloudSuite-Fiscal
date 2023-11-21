using CloudSuite.Modules.Domain.Models;

namespace CloudSuite.Modules.Domain.Contracts
{
    public interface IIdeCancelamentoRepository
    {
        Task<IdeCancelamento> GetByCancelOrder(string cancelOrder);

        Task<IdeCancelamento> GetByCancelReason(string cancelReason);

        Task<IdeCancelamento> GetByTimeDate(DateTimeOffset timeDSate);
        
        Task<IEnumerable<IdeCancelamento>> GetList();

        Task Add(IdeCancelamento ideCancelamento);

        void Update(IdeCancelamento ideCancelamento);

        void Remove(IdeCancelamento ideCancelamento);
        
         
    }
}