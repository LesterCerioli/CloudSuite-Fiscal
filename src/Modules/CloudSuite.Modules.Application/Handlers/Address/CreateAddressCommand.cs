using CloudSuite.Modules.Application.Handlers.Address.Responses;
using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressEntity = CloudSuite.Modules.Domain.Models.Address;
using CityEntity = CloudSuite.Modules.Domain.Models.City;
using DistrictEntity = CloudSuite.Modules.Domain.Models.District;

namespace CloudSuite.Modules.Application.Handlers.Address
{
    public class CreateAddressCommand : IRequest<CreateAddressResponse>
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(100)]
        public string? ContactName { get; private set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(450)]
        public string? AddressLine1 { get; private set; }

        public CityEntity City { get; private set; }

        public DistrictEntity District { get; private set; }

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
