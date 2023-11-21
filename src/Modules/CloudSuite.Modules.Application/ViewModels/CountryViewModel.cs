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
        public Guid Id { get; private set; }

        [DisplayName("Nome do País")]
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(450)]
        public string? CountryName { get; private set; }

        [DisplayName("Codigo")]
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(450)]
        public string? Code3 { get; private set; }

        [DisplayName("Status da Cobrança")]
        public bool? IsBillingEnabled { get; private set; }

        [DisplayName("Status da Envio")]
        public bool? IsShippingEnabled { get; private set; }

        [DisplayName("Status da Cidade")]
        public bool? IsCityEnabled { get; private set; }

        [DisplayName("Status do Codigo Postal")]
        public bool? IsZipCodeEnabled { get; private set; }

        [DisplayName("Status da Distrito")]
        public bool? IsDistrictEnabled { get; private set; }

        [DisplayName("Id do Estado")]
        public Guid StateId { get; private set; }
    }
}
