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
    public class DarfViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Mês de Referencia do Darf")]
        [Required(ErrorMessage = "The field is required.")]
        public string ReferenceMonth { get; set; }

        [DisplayName("Data de Vencimento do Darf")]
        [Required(ErrorMessage = "The field is required.")]
        public DateTime DueDate { get; private set; }

        [DisplayName("Ano de Referencia do Darf")]
        [Required(ErrorMessage = "The field is required.")]
        public string ReferenceYear { get; set; }

        [DisplayName("Valor de Pagamento no Darf")]
        [Required(ErrorMessage = "The field is required.")]
        public decimal DarfPaymentValue { get; set; }

        [DisplayName("Recibo Declarado no Darf")]
        [Required(ErrorMessage = "The field is required.")]
        public string RecuboDeclaroNumero { get; set; }

        [DisplayName("Numero do Documento no Darf")]
        [Required(ErrorMessage = "The field is required.")]
        public string DocumentNumber { get; set; }

        [DisplayName("Codigo de Barras do Darf")]
        [Required(ErrorMessage = "The field is required.")]
        public string BarCode { get; set; }

        [DisplayName("Data de Validade do Darf")]
        [Required(ErrorMessage = "The field is required.")]
        public DateTime ValidationDate { get; set; }

        [DisplayName("Perido de Apuração do Darf")]
        [Required(ErrorMessage = "The field is required.")]
        public DateTime PeriodoApuracao { get; set; }

        [DisplayName("Codigo da Receito do Darf")]
        [Required(ErrorMessage = "The field is required.")]
        public string ReceitaCode { get; set; }

        [DisplayName("Valor Principal do Darf")]
        [Required(ErrorMessage = "The field is required.")]
        public string MainValue { get; set; }

        [DisplayName("Quantidade de Multa do Darf")]
        [Required(ErrorMessage = "The field is required.")]
        public decimal AmountFine { get; set; }

        [DisplayName("Tipo de Pagamento do Darf")]
        [Required(ErrorMessage = "The field is required.")]
        public bool IsInstallment { get; set; }

        [DisplayName("Taxa de Juros do Darf")]
        [Required(ErrorMessage = "The field is required.")]
        public decimal Interest { get; set; }

        [DisplayName("Valor total do Darf")]
        [Required(ErrorMessage = "The field is required.")]
        public decimal TotalValue { get; set; }

    }
}
