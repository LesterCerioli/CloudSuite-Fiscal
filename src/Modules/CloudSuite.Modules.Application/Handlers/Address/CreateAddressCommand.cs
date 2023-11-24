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
        public Guid Id { get; private set; }

        public string? ContactName { get; set; }

        public string? AddressLine1 { get; set; }

        public CityEntity? City { get; set; }

        public DistrictEntity? District { get; set; }

        public Guid DistrictId { get; set; }

        public CreateAddressCommand()
        {
            Id = Guid.NewGuid();
        }

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
