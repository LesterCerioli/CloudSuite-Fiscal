using CloudSuite.Modules.Domain.Models;

namespace CloudSuite.Modules.Domain.Contracts
{
    public interface IDistrictRepository
    {
         Task<District> GetByName(string name);

         Task<IEnumerable<District>> GetList();

         Task Add(District district);

         void Update(District district);

         void Remove(District district);
    }
}