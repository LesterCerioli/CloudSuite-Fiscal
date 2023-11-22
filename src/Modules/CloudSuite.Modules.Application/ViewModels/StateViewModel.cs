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
    public class StateViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nome do Estado")]
        [Required(ErrorMessage = "Campo StateName é obrigatorio.")]
        [StringLength(100)]
        public string StateName { get; set; }

        [DisplayName("Unidade Federativa")]
        [Required(ErrorMessage = "Campo UF é obrigatorio.")]
        public string UF { get; set; }

        [DisplayName("País")]
        [Required(ErrorMessage = "Campo Country é obrigatorio.")]
        public string Country { get; set; }

        [DisplayName("País")]
        [Required(ErrorMessage = "Campo Id do País é obrigatorio.")]
        public Guid CountryId { get; set; }

    }
}
