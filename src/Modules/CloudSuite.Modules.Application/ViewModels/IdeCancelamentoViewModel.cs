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
        public CancelOrder CancelOrder { get; private set; }

        [DisplayName("Razão do Cancelamento")]
        public string? CancelReason { get; private set; }

        [DisplayName("Hora do Cancelamento")]
        public DateTimeOffset? TimeDate { get; private set; }

    }
}
