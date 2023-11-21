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
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [StringLength(100)]
        public string? StateName { get; private set; }

        [DisplayName("Unidade Federativa")]
        [Required(ErrorMessage = "Este cmapo é de preenchimento obrigatório.")]
        public string? UF { get; private set; }

        [DisplayName("País")]
        public Country Country { get; private set; }

        [DisplayName("Id do País")]
        public Guid CountryId { get; private set; }
    }
}
