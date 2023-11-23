using CloudSuite.Modules.Application.Handlers.Country.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Country.Requests
{
    public class CheckCountryExistsByNameRequest : IRequest<CheckCountryExistsByNameResponse>
    {

        public Guid Id { get; set; }

        public string? CountryName { get; private set; }

        public CheckCountryExistsByNameRequest(Guid id, string? countryName)
        {
            Id = Guid.NewGuid();
            CountryName = countryName;
        }
    }
}
