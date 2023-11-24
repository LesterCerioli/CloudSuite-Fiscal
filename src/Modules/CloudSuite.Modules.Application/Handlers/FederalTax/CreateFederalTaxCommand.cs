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
        public Guid Id { get; private set; }

        public decimal VPIS { get; set; }

        public decimal VCOFINS { get; set; }

        public decimal VIR { get; set; }

        public decimal VINSS { get; set; }

        public decimal VCSLL { get; set; }

        public bool VPISSpecified { get; set; }

        public bool VCOFINSSpecified { get; set; }

        public bool VIRSpecified { get; set; }

        public bool VINSSSpecified { get; set; }

        public bool VCSLLSpecified { get; set; }

        public CreateFederalTaxCommand()
        {
            Id = Guid.NewGuid();
        }

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
