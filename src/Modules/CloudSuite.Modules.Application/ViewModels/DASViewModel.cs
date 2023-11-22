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
        [Required(ErrorMessage = "The field is required.")]
        public string ReferenceMonth { get; set; }

        [DisplayName("Data de Vencimento do DAS")]
        [Required(ErrorMessage = "The field is required.")]
        public DateTime DueDate { get; set; }

        [DisplayName("Ano de Referencia do DAS")]
        [Required(ErrorMessage = "The field is required.")]
        public string ReferenceYear { get; set; }

        [DisplayName("Valor de Pagamento do DAS")]
        [Required(ErrorMessage = "The field is required.")]
        public string PaymentValue { get; set; }

        [DisplayName("Numero do Documento no DAS")]
        [Required(ErrorMessage = "The field is required.")]
        public string DocumentNumber { get;  set; }

        [DisplayName("Codigo de Barra do DAS")]
        [Required(ErrorMessage = "The field is required.")]
        public string BarCode { get; set; }

    }
}
