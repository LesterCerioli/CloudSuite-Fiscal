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
        public Guid Id { get; set; }

        public string? CountryName { get; private set; }

        public string? Code3 { get; private set; }

        public bool? IsBillingEnabled { get; private set; }

        public bool? IsShippingEnabled { get; private set; }

        public bool? IsCityEnabled { get; private set; }

        public bool? IsZipCodeEnabled { get; private set; }

        public bool? IsDistrictEnabled { get; private set; }

        public Guid StateId { get; private set; }

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
