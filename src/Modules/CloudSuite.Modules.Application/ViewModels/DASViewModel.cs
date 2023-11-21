using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.ViewModels
{
    public class DASViewModel
    {
        [Key]
        public Guid Id { get; private set; }

        [DisplayName("Mês de Referencia do DAS")]
        public string? ReferenceMonth { get; private set; }

        [DisplayName("Data de Vencimento do DAS")]
        public DateTime? DueDate { get; private set; }

        [DisplayName("Ano de Referencia do DAS")]
        public string? ReferenceYear { get; private set; }

        [DisplayName("Valor de Pagamento do DAS")]
        public string? PaymentValue { get; private set; }

        [DisplayName("Numero do Documento no DAS")]
        public string? DocumentNumber { get; private set; }

        [DisplayName("Codigo de Barra do DAS")]
        public string? BarCode { get; private set; }

    }
}
