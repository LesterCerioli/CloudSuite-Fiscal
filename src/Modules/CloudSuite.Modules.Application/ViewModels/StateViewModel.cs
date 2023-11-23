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
        public Guid Id { get; private set; }

        [DisplayName("Nome do Estado")]
        [Required(ErrorMessage = "Campo Nome do Estado é obrigatorio.")]
        [StringLength(100)]
        public string StateName { get; set; }

        [DisplayName("Unidade Federativa")]
        [Required(ErrorMessage = "Campo Unidade Federativa é obrigatorio.")]
        public string UF { get; set; }

        [DisplayName("País")]
        [Required(ErrorMessage = "Campo País é obrigatorio.")]
        public string Country { get; set; }

    }
}
