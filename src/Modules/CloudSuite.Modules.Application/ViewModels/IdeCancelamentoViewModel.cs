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
    public class IdeCancelamentoViewModel
    {
        [Key]
        public Guid Id { get; private set; }

        [DisplayName("Ordem de Cancelamento")]
        [Required(ErrorMessage = "Campo Ordem de Cancelamento é obrigatorio.")]
        public string CancelOrder { get; set; }

        [DisplayName("Razão do Cancelamento")]
        [Required(ErrorMessage = "Campo Razão do Cancelamento é obrigatorio.")]
        public string CancelReason { get; set; }

        [DisplayName("Hora do Cancelamento")]
        [Required(ErrorMessage = "Campo Hora do Cancelamento é obrigatorio.")]
        public DateTimeOffset TimeDate { get; set; }

    }
}
