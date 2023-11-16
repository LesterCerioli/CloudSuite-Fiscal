using CloudSuite.Modules.Domain.Models;

namespace CloudSuite.Modules.Domain.Contracts
{
    public interface IStateRepository
    {
        Task<State> GetByStateName(string stateName);

        Task<State> GetByUF(string uf);

        Task<IEnumerable<State>> GetList();

        Task Add(State state);

        void Update(State state);

        void Remove(State state);
         
    }
}