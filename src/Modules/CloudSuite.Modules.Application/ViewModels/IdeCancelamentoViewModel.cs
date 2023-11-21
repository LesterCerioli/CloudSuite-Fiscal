using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
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

        public CancelOrder CancelOrder { get; private set; }

        public string? CancelReason { get; private set; }

        public DateTimeOffset? TimeDate { get; private set; }

    }
}
