using CloudSuite.Modules.Domain.Models;

namespace CloudSuite.Modules.Domain.Contracts
{
    public interface IAddressRepository
    {
        Task<Address> GetByAddressLine1(string addressLine1);

        Task<IEnumerable<Address>> GetList();

        Task Add(Address address);

        void Update(Address address);

        void Remove(Address address);


    }
}