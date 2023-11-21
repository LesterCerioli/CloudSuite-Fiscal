using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("VPIS dos Tributos Federais")]
        public decimal? VPIS { get; private set; }

        [DisplayName("VCOFINS dos Tributos Federais")]
        public decimal? VCOFINS { get; private set; }

        [DisplayName("VIR dos Tributos Federais")]
        public decimal? VIR { get; private set; }

        [DisplayName("VINSS dos Tributos Federais")]
        public decimal? VINSS { get; private set; }

        [DisplayName("VCSLL dos Tributos Federais")]
        public decimal? VCSLL { get; private set; }

        [DisplayName("VPISSpecified dos Tributos Federais")]
        public bool VPISSpecified { get; private set; }

        [DisplayName("VCOFINSSpecified dos Tributos Federais")]
        public bool VCOFINSSpecified { get; private set; }

        [DisplayName("VIRSpecified dos Tributos Federais")]
        public bool VIRSpecified { get; private set; }

        [DisplayName("VINSSSpecified dos Tributos Federais")]
        public bool VINSSSpecified { get; private set; }

        [DisplayName("VCSLLSpecified dos Tributos Federais")]
        public bool VCSLLSpecified { get; private set; }
    }
}
