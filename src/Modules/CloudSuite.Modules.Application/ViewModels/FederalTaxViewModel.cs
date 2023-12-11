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
        [Required(ErrorMessage = "Campo VPIS é obrigatorio.")]
        public decimal VPIS { get; set; }

        [DisplayName("VCOFINS dos Tributos Federais")]
        [Required(ErrorMessage = "Campo VCOFINS é obrigatorio.")]
        public decimal VCOFINS { get; set; }

        [DisplayName("VIR dos Tributos Federais")]
        [Required(ErrorMessage = "Campo VIR é obrigatorio.")]
        public decimal VIR { get; set; }

        [DisplayName("VINSS dos Tributos Federais")]
        [Required(ErrorMessage = "Campo VINSS é obrigatorio.")]
        public decimal VINSS { get; set; }

        [DisplayName("VCSLL dos Tributos Federais")]
        [Required(ErrorMessage = "Campo VCSLL é obrigatorio.")]
        public decimal VCSLL { get; set; }

        [DisplayName("VPISSpecified dos Tributos Federais")]
        [Required(ErrorMessage = "Campo VPISSpecified é obrigatorio.")]
        public bool VPISSpecified { get; set; }

        [DisplayName("VCOFINSSpecified dos Tributos Federais")]
        [Required(ErrorMessage = "Campo VCOFINSSpecified é obrigatorio.")]
        public bool VCOFINSSpecified { get; set; }

        [DisplayName("VIRSpecified dos Tributos Federais")]
        [Required(ErrorMessage = "Campo VIRSpecified é obrigatorio.")]
        public bool VIRSpecified { get; set; }

        [DisplayName("VINSSSpecified dos Tributos Federais")]
        [Required(ErrorMessage = "Campo VINSSSpecified é obrigatorio.")]
        public bool VINSSSpecified { get; set; }

        [DisplayName("VCSLLSpecified dos Tributos Federais")]
        [Required(ErrorMessage = "Campo é VCSLLSpecified obrigatorio.")]
        public bool VCSLLSpecified { get; set; }
    }
}
