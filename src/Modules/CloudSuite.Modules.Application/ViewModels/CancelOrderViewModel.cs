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
        [Required(ErrorMessage = "The {0} field is required.")]
        public DateTimeOffset RequestDate { get; set; }

        [DisplayName("Cnpj do pedido")]
        [Required(ErrorMessage = "The {0} field is required.")]
        public Cnpj Cnpj { get; set; }
    }
}
