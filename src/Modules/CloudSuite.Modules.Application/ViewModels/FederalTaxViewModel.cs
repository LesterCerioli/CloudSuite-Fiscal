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
        public Guid Id { get; set; }

        [DisplayName("VPIS dos Tributos Federais")]
        [Required(ErrorMessage = "The field is required.")]
        public decimal VPIS { get; set; }

        [DisplayName("VCOFINS dos Tributos Federais")]
        [Required(ErrorMessage = "The field is required.")]
        public decimal VCOFINS { get; set; }

        [DisplayName("VIR dos Tributos Federais")]
        [Required(ErrorMessage = "The field is required.")]
        public decimal VIR { get; set; }

        [DisplayName("VINSS dos Tributos Federais")]
        [Required(ErrorMessage = "The field is required.")]
        public decimal VINSS { get; set; }

        [DisplayName("VCSLL dos Tributos Federais")]
        [Required(ErrorMessage = "The field is required.")]
        public decimal VCSLL { get; set; }

        [DisplayName("VPISSpecified dos Tributos Federais")]
        [Required(ErrorMessage = "The field is required.")]
        public bool VPISSpecified { get; set; }

        [DisplayName("VCOFINSSpecified dos Tributos Federais")]
        [Required(ErrorMessage = "The field is required.")]
        public bool VCOFINSSpecified { get; set; }

        [DisplayName("VIRSpecified dos Tributos Federais")]
        [Required(ErrorMessage = "The field is required.")]
        public bool VIRSpecified { get; set; }

        [DisplayName("VINSSSpecified dos Tributos Federais")]
        [Required(ErrorMessage = "The field is required.")]
        public bool VINSSSpecified { get; set; }

        [DisplayName("VCSLLSpecified dos Tributos Federais")]
        [Required(ErrorMessage = "The field is required.")]
        public bool VCSLLSpecified { get; set; }
    }
}
