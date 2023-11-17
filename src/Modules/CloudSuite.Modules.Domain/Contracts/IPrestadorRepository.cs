using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Models;

namespace CloudSuite.Modules.Domain.Contracts
{
    public interface IPrestadorRepository
    {

        Task<Prestador> GetByCnpj(Cnpj cnpj);
        
        Task<Prestador> GetByInscricaoMunicipal(string inscricaoMunicipal);

        Task<Prestador> GetByInscricaoEstadual(string inscricaoEstadual);

        Task<Prestador> GetByDocTomadorEstrangeiro(string docTomadorEstrangeiro);

        Task<Prestador> GetByNomeFantasia(string nomeFantasia);

        Task<IEnumerable<Prestador>> GetList();

        Task Add(Prestador prestador);

        void Update(Prestador prestador);

        void Remove(Prestador prestador);

         
    }
}