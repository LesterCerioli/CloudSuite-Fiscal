using CloudSuite.Modules.Domain.Models;

namespace CloudSuite.Modules.Domain.Contracts
{
    public interface IDASRepository
    {

        Task<DAS> GetByReferenceMonth(string referenceMonth);

        Task<DAS> GetByDueDate(DateTime? dueDate);
        
        Task<DAS> GetByDocumentNumber(string documentNumber);

        Task<DAS> GetByReferenceYear(string referenceYear);
        
        Task<IEnumerable<DAS>> GetList();

        Task Add(DAS das);

        void Update(DAS das)         ;

        void Remove(DAS das);
    }
}