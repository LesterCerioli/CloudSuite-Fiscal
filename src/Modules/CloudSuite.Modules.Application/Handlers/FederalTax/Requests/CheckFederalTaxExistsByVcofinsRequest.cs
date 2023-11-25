using CloudSuite.Modules.Application.Handlers.FederalTax.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.FederalTax.Requests
{
    public class CheckFederalTaxExistsByVcofinsRequest : IRequest<CheckFederalTaxExistsByVcofinsResponse>
    {
        
        public Guid Id { get; private set; }

        public decimal VCOFINS { get; set; }

        public CheckFederalTaxExistsByVcofinsRequest(decimal vCOFINS)
        {
            Id = Guid.NewGuid();
            VCOFINS = vCOFINS;
        }
    }
}
