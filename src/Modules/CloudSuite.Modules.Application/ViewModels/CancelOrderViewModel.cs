using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.ViewModels
{
    public class CancelOrderViewModel
    {
        [Key]
        public Guid Id { get; private set; }

        public IdeCancelamento IdeCancelamento { get; private set; }

        public DateTimeOffset? RequestDate { get; private set; }

        public Cnpj Cnpj { get; private set; }
    }
}
