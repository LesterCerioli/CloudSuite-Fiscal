using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressEntity = CloudSuite.Modules.Domain.Models.Address;

namespace CloudSuite.Modules.Application.Handlers.Address
{
    public class CreateAddressCommand
    {
        public Guid Id { get; set; }

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


        public AddressEntity GetEntity()
        {
            return new AddressEntity(
                this.Id,
                this.City,
                this.District,
                this.ContactName,
                this.AddressLine1
                );
        }
    }
}
