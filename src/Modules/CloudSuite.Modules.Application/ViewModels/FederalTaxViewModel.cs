using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.ViewModels
{
    public class FederalTaxViewModel
    {
        [Key]
        public Guid Id { get; private set; }

        public decimal? VPIS { get; private set; }

        public decimal? VCOFINS { get; private set; }

        public decimal? VIR { get; private set; }

        public decimal? VINSS { get; private set; }

        public decimal? VCSLL { get; private set; }

        public bool VPISSpecified { get; private set; }

        public bool VCOFINSSpecified { get; private set; }

        public bool VIRSpecified { get; private set; }

        public bool VINSSSpecified { get; private set; }

        public bool VCSLLSpecified { get; private set; }
    }
}
