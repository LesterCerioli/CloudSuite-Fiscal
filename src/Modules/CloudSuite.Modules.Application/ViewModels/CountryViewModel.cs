using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.ViewModels
{
    public class CountryViewModel
    {

        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nome do País")]
        [Required(ErrorMessage = "Campo Nome do País é obrigatorio.")]
        [StringLength(450)]
        public string CountryName { get; set; }

        [DisplayName("Codigo")]
        [Required(ErrorMessage = "Campo Codigo é obrigatorio.")]
        [StringLength(450)]
        public string Code3 { get; set; }

        [DisplayName("Status da Cobrança")]
        [Required(ErrorMessage = "Campo Status da Cobrança é obrigatorio.")]
        public bool IsBillingEnabled { get; set; }

        [DisplayName("Status da Envio")]
        [Required(ErrorMessage = "Campo Status da Envio é obrigatorio.")]
        public bool IsShippingEnabled { get; set; }

        [DisplayName("Status da Cidade")]
        [Required(ErrorMessage = "Campo Status da Cidade é obrigatorio.")]
        public bool IsCityEnabled { get; set; }

        [DisplayName("Status do Codigo Postal")]
        [Required(ErrorMessage = "Campo Status do Codigo Postal é obrigatorio.")]
        public bool IsZipCodeEnabled { get; set; }

        [DisplayName("Status da Distrito")]
        [Required(ErrorMessage = "Campo Status da Distrito é obrigatorio.")]
        public bool IsDistrictEnabled { get; set; }

    }
}
