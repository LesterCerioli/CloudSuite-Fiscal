using CloudSuite.Modules.Application.Handlers.FederalTax.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FederalTaxEntity = CloudSuite.Modules.Domain.Models.FederalTax;

namespace CloudSuite.Modules.Application.Handlers.FederalTax
{
    public class CreateFederalTaxCommand : IRequest<CreateFederalTaxResponse>
    {
        public Guid Id { get; set; }

        public decimal VPIS { get; private set; }

        public decimal VCOFINS { get; private set; }

        public decimal VIR { get; private set; }

        public decimal VINSS { get; private set; }

        public decimal VCSLL { get; private set; }

        public bool VPISSpecified { get; private set; }

        public bool VCOFINSSpecified { get; private set; }

        public bool VIRSpecified { get; private set; }

        public bool VINSSSpecified { get; private set; }

        public bool VCSLLSpecified { get; private set; }

        public FederalTaxEntity GetEntity()
        {
            return new FederalTaxEntity(
                this.VPIS,
                this.VCOFINS,
                this.VIR,
                this.VINSS,
                this.VCSLL,
                this.VPISSpecified,
                this.VCOFINSSpecified,
                this.VIRSpecified,
                this.VINSSSpecified,
                this.VCSLLSpecified
                );
        }
    }
}
