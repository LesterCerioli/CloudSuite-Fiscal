using CloudSuite.Modules.Application.Handlers.FederalTax.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.FederalTax.Requests
{
    public class CheckFederalTaxExistsByVirRequest : IRequest<CheckFederalTaxExistsByVirResponse>
    {

        public Guid Id { get; private set; }

        public decimal VIR { get; private set; }

        public CheckFederalTaxExistsByVirRequest(Guid id, decimal vIR)
        {
            Id = Guid.NewGuid();
            VIR = vIR;
        }
    }
}
