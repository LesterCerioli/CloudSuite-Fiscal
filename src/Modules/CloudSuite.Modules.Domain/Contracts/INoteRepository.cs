using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Models;

namespace CloudSuite.Modules.Domain.Contracts
{
    public interface INoteRepository
    {
        Task<Note> GetByCnpj(Cnpj cnpj);

        Task<Note> GetByNoteNumber(string noteNumber);

        Task<Note> GetByValue(decimal value);

        Task<Note> GetByEmissionDate(DateTime? date);

        Task<IEnumerable<Note>> GetList();

        Task Add(Note note);

        void Update(Note note);

        void Remove(Note note);
         
    }
}