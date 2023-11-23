using CloudSuite.Modules.Application.Handlers.FederalTax.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.FederalTax.Requests
{
    public class CheckFederalTaxExistsByVpisRequest : IRequest<CheckFederalTaxExistsByVpisResponse>
    {

        public Guid Id { get; private set; }

        public decimal VPIS { get; private set; }

        public CheckFederalTaxExistsByVpisRequest(Guid id, decimal vPIS)
        {
            Id = Guid.NewGuid();
            VPIS = vPIS;
        }
    }
}
