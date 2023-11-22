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
        [Required(ErrorMessage = "The field is required.")]
        [StringLength(450)]
        public string CountryName { get; set; }

        [DisplayName("Codigo")]
        [Required(ErrorMessage = "The field is required.")]
        [StringLength(450)]
        public string Code3 { get; set; }

        [DisplayName("Status da Cobrança")]
        [Required(ErrorMessage = "The field is required.")]
        public bool IsBillingEnabled { get; set; }

        [DisplayName("Status da Envio")]
        [Required(ErrorMessage = "The field is required.")]
        public bool IsShippingEnabled { get; set; }

        [DisplayName("Status da Cidade")]
        [Required(ErrorMessage = "The field is required.")]
        public bool IsCityEnabled { get; set; }

        [DisplayName("Status do Codigo Postal")]
        [Required(ErrorMessage = "The field is required.")]
        public bool IsZipCodeEnabled { get; set; }

        [DisplayName("Status da Distrito")]
        [Required(ErrorMessage = "The field is required.")]
        public bool IsDistrictEnabled { get; set; }

    }
}
