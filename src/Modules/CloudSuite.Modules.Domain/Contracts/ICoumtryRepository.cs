using CloudSuite.Modules.Domain.Models;

namespace CloudSuite.Modules.Domain.Contracts
{
    public interface ICoumtryRepository
    {
        Task<Country> GetbyCountryName(string countryName);

        Task<IEnumerable<Country>> GetList();

        Task Add(Country country);

        void Update(Country country);

        void Remove(Country country);   

    }
}