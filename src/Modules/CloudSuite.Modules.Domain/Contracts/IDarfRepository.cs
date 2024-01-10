using System.Data;
using CloudSuite.Modules.Domain.Models;

namespace CloudSuite.Modules.Domain.Contracts
{
    public interface IDarfRepository
    {
        Task<Darf> GetByReferenceMonth(string referenceMonth);

        Task<Darf> GetByDueDate(DateTime? duedate) ;

        Task<Darf> GetByDocumentNumber(string documentNumber);

        Task<Darf> GetByValidationDate(DateTime? validationDate);

        Task<IEnumerable<Darf>> GetList();

        Task Add(Darf darf);

        void Update(Darf darf);

        void Remove(Darf darf);
    }
}