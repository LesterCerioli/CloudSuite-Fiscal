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
        [Required(ErrorMessage = "The field is required.")]
        [StringLength(100)]
        public string StateName { get; set; }

        [DisplayName("Unidade Federativa")]
        [Required(ErrorMessage = "The field is required.")]
        public string UF { get; set; }

    }
}
