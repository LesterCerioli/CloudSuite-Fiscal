using CloudSuite.Modules.Domain.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CloudSuite.Modules.Application.ViewModels
{
    public class DarfViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Mês de Referencia do Darf")]
        [Required(ErrorMessage = "Campo Reference Month é obrigatorio.")]
        public string ReferenceMonth { get; set; }

        [DisplayName("Data de Vencimento do Darf")]
        [Required(ErrorMessage = "Campo Due Date é obrigatorio.")]
        public DateTime DueDate { get; private set; }

        [DisplayName("Ano de Referencia do Darf")]
        [Required(ErrorMessage = "Campo Reference Year é obrigatorio.")]
        public string ReferenceYear { get; set; }

        [DisplayName("Valor de Pagamento no Darf")]
        [Required(ErrorMessage = "Campo Darf Payment Value é obrigatorio.")]
        public decimal DarfPaymentValue { get; set; }

        [DisplayName("Recibo Declarado no Darf")]
        [Required(ErrorMessage = "Campo RecuboDeclaroNumero é obrigatorio.")]
        public string RecuboDeclaroNumero { get; set; }

        [DisplayName("Numero do Documento no Darf")]
        [Required(ErrorMessage = "Campo Document Number é obrigatorio.")]
        public string DocumentNumber { get; set; }

        [DisplayName("Codigo de Barras do Darf")]
        [Required(ErrorMessage = "Campo Bar Code é obrigatorio.")]
        public string BarCode { get; set; }

        [DisplayName("Data de Validade do Darf")]
        [Required(ErrorMessage = "Campo Validation Date é obrigatorio.")]
        public DateTime ValidationDate { get; set; }

        [DisplayName("Perido de Apuração do Darf")]
        [Required(ErrorMessage = "Campo Periodo Apuracao é obrigatorio.")]
        public DateTime PeriodoApuracao { get; set; }

        [DisplayName("Cnpj do Darf")]
        [Required(ErrorMessage = "Campo Cnpj é obrigatorio.")]
        public string Cnpj { get; set; }

        [DisplayName("Codigo da Receito do Darf")]
        [Required(ErrorMessage = "Campo Receita Code é obrigatorio.")]
        public string ReceitaCode { get; set; }

        [DisplayName("Valor Principal do Darf")]
        [Required(ErrorMessage = "Campo Main Value é obrigatorio.")]
        public string MainValue { get; set; }

        [DisplayName("Quantidade de Multa do Darf")]
        [Required(ErrorMessage = "Campo Amount Fine é obrigatorio.")]
        public decimal AmountFine { get; set; }

        [DisplayName("Tipo de Pagamento do Darf")]
        [Required(ErrorMessage = "Campo Is Installment é obrigatorio.")]
        public bool IsInstallment { get; set; }

        [DisplayName("Taxa de Juros do Darf")]
        [Required(ErrorMessage = "Campo Interest é obrigatorio.")]
        public decimal Interest { get; set; }

        [DisplayName("Valor total do Darf")]
        [Required(ErrorMessage = "Campo Total Value é obrigatorio.")]
        public decimal TotalValue { get; set; }

        [DisplayName("Prestador de Serviço")]
        [Required(ErrorMessage = "Campo Prestador é obrigatorio.")]
        public string Prestador { get; private set; }

    }
}
