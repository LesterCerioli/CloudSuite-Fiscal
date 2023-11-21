using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
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

        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [StringLength(100)]
        public string? StateName { get; private set; }

        [Required(ErrorMessage = "Este cmapo é de preenchimento obrigatório.")]

        public string? UF { get; private set; }

        public Country Country { get; private set; }

        public Guid CountryId { get; private set; }
    }
}
