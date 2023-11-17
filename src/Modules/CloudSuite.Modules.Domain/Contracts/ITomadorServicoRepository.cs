using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Models;

namespace CloudSuite.Modules.Domain.Contracts
{
    public interface ITomadorServicoRepository
    {
        Task<TomadorServico> GetByCnpj(Cnpj cnpj);

        Task<TomadorServico> GetBySocialReason(string socialReason);

        Task<IEnumerable<TomadorServico>> GetList();

        Task Add(TomadorServico tomadorServico);

        void Update(TomadorServico tomadorServico);

        void Remove(TomadorServico tomadorServico);

    }
}