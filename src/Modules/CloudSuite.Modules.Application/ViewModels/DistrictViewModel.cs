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

        [DisplayName("Estado")]
        [Required(ErrorMessage = "Campo Estado é obrigatorio.")]
        [StringLength(450)]
        public string State { get; set; }

        [DisplayName("Nome do Distrito")]
        [Required(ErrorMessage = "Campo Nome do Distrito é obrigatorio.")]
        [StringLength(450)]
        public string Name { get; set; }

        [DisplayName("Tipo do Distrito")]
        [Required(ErrorMessage = "Campo Tipo do Distrito é obrigatorio.")]
        public string Type { get; set; }

        [DisplayName("Localização do Distrito")]
        [Required(ErrorMessage = "Campo Localização do Distrito é obrigatorio.")]
        [StringLength(100)]
        public string Location { get; set; }
    }
}
