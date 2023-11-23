using CloudSuite.Modules.Application.Handlers.FederalTax.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.FederalTax.Requests
{
    public class CheckFederalTaxExistsByVinssRequest : IRequest<CheckFederalTaxExistsByVinssResponse>
    {

        public Guid Id { get; private set; }

        public decimal VINSS { get; set; }

        public CheckFederalTaxExistsByVinssRequest(decimal vINSS)
        {
            Id = Guid.NewGuid();
            VINSS = vINSS;
        }
    }
}
