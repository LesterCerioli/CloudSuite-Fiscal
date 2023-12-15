using CloudSuite.Modules.Common.ValueObjects;
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
    public class CancelOrderViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Data da Requisição")]
        [Required(ErrorMessage = "Campo Data da Requisição é obrigatorio.")]
        public DateTimeOffset RequestDate { get; set; }

        [DisplayName("Cnpj")]
        [Required(ErrorMessage = "Campo Cnpj é obrigatorio.")]
        public string Cnpj { get; set; }

    }
}
