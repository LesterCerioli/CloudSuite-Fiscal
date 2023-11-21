using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.ViewModels
{
    public class CityViewModel
    {
        [Key]
        public Guid Id { get; private set; }

        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [MaxLength(100)]
        public string? CityName { get; private set; }

        public State State { get; private set; }

        public Guid StateId { get; private set; }
    }
}
