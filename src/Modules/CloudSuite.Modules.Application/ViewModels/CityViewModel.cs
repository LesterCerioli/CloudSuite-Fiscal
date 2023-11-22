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
    public class CityViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nome da Cidade")]
        [Required(ErrorMessage = "Campo Nome da Cidade é obrigatorio.")]
        [MaxLength(100)]
        public string CityName { get; set; }

        [DisplayName("Nome do Estado")]
        [Required(ErrorMessage = "Campo Nome do Estado é obrigatorio.")]
        public string State { get; set; }

    }
}
