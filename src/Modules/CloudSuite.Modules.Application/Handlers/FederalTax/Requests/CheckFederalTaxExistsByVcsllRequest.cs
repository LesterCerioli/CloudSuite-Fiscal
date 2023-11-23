using CloudSuite.Modules.Application.Handlers.FederalTax.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.FederalTax.Requests
{
    public class CheckFederalTaxExistsByVcsllRequest : IRequest<CheckFederalTaxExistsByVcsllResponse>
    {

        public Guid Id { get; private set; }

        public decimal VCSLL { get; private set; }

        public CheckFederalTaxExistsByVcsllRequest(Guid id, decimal vCSLL)
        {
            Id = Guid.NewGuid();
            VCSLL = vCSLL;
        }
    }
}
