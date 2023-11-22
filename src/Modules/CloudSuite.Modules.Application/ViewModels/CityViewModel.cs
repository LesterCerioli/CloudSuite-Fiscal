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
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [MaxLength(100)]
        public string CityName { get; set; }
    }
}
