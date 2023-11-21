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
        public Guid Id { get; private set; }
        
        [DisplayName("Id de Cancelamento")]
        public IdeCancelamento IdeCancelamento { get; private set; }

        [DisplayName("Data da Requisição")]
        public DateTimeOffset? RequestDate { get; private set; }

        [DisplayName("Cnpj do pedido")]
        public Cnpj Cnpj { get; private set; }
    }
}
