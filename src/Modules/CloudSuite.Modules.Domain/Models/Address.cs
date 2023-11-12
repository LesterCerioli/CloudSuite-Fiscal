using System.ComponentModel.DataAnnotations;
using CloudSuite.Modules.Common.ValueObjects;
using NetDevPack.Domain;


namespace CloudSuite.Modules.Domain.Models
{
    public class Address : Entity, IAggregateRoot
    {
        private readonly List<District> _districts = new List<District>();

        private readonly List<City> _cities = new List<City>();

        public Address(Guid id, City city, District district, string contactName, string adressLine1) {
            Id = Guid.NewGuid();
            _districts = new List<District>();
            _cities = new List<City>();
            City = city;
            District = district;
            ContactName = contactName;
            AddressLine1 = adressLine1;
        }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(100)]
        public string? ContactName { get; private set; }

        public PostalCode PostalCodeal { get; set; }
        
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(450)]
        public string? AddressLine1 { get; private set; }

        public City City { get; private set; }

        public District District { get; private set; }

        public Guid DistrictId { get; private set; }

        public IReadOnlyCollection<City> Cities => _cities.AsReadOnly();

        public IReadOnlyCollection<District> Districts => _districts.AsReadOnly();
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}