using CloudSuite.Modules.Application.Handlers.Country.Responses;
using CloudSuite.Modules.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountryEntity = CloudSuite.Modules.Domain.Models.Country;

namespace CloudSuite.Modules.Application.Handlers.Country
{
    public class CreateCountryCommand : IRequest<CreateCountryResponse>
    {
        public Guid Id { get; private set; }

        public string? CountryName { get; set; }

        public string? Code3 { get; set; }

        public bool? IsBillingEnabled { get; set; }

        public bool? IsShippingEnabled { get; set; }

        public bool? IsCityEnabled { get; set; }

        public bool? IsZipCodeEnabled { get; set; }

        public bool? IsDistrictEnabled { get; set; }

        public Guid StateId { get; set; }

        public CreateCountryCommand()
        {
            Id = Guid.NewGuid();
        }

        public CountryEntity GetEntity()
        {
            return new CountryEntity(
                this.CountryName,
                this.Code3,
                this.IsBillingEnabled,
                this.IsShippingEnabled,
                this.IsCityEnabled,
                this.IsZipCodeEnabled,
                this.IsDistrictEnabled
                );
        }
    }
}
