using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Domain.Contracts
{
    public interface IPessoaRepository
    {
        Task<Pessoa> GetByName(string name);

        Task<IEnumerable<Pessoa>> GetList();

        Task Add(Pessoa pessoa);

        void Update(Pessoa pessoa);

        void Remove(Pessoa pessoa);

    }
}
