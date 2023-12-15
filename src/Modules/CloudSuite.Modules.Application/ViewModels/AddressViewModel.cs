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
    public class AddressViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nome do Contato")]
        [Required(ErrorMessage = "Campo Nome do Contato é obrigatorio.")]
        [StringLength(100)]
        public string ContactName { get; set; }

        [DisplayName("Endereço Completo")]
        [Required(ErrorMessage = "Campo Endereço Completo é obrigatorio.")]
        [StringLength(450)]
        public string AddressLine { get; set; }

    }
}
