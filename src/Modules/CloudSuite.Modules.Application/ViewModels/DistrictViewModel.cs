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
    public class DistrictViewModel
    {
        [Key]
        public Guid Id { get; private set; }

        [DisplayName("Estado do Distrito")]
        public State State { get; set; }

        [DisplayName("Id do Estado")]
        public Guid StateId { get; private set; }

        [DisplayName("Nome do Distrito")]
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(450)]
        public string? Name { get; private set; }

        [DisplayName("Tipo do Distrito")]
        [Required(ErrorMessage = "The {0} field is required.")]
        public string? Type { get; private set; }

        [DisplayName("Localização do Distrito")]
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(100)]
        public string? Location { get; private set; }
    }
}
