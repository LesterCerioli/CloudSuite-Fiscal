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
        [Required(ErrorMessage = "Campo Mês de Referencia é obrigatorio.")]
        public string ReferenceMonth { get; set; }

        [DisplayName("Data de Vencimento do Darf")]
        [Required(ErrorMessage = "Campo Data de Vencimento é obrigatorio.")]
        public DateTime? DueDate { get; set; }

        [DisplayName("Ano de Referencia do Darf")]
        [Required(ErrorMessage = "Campo Ano de Referencia é obrigatorio.")]
        public string ReferenceYear { get; set; }

        [DisplayName("Valor de Pagamento no Darf")]
        [Required(ErrorMessage = "Campo Valor de Pagamento é obrigatorio.")]
        public decimal DarfPaymentValue { get; set; }

        [DisplayName("Recibo Declarado no Darf")]
        [Required(ErrorMessage = "Campo Recibo Declarado é obrigatorio.")]
        public string RecuboDeclaroNumero { get; set; }

        [DisplayName("Numero do Documento no Darf")]
        [Required(ErrorMessage = "Campo Numero do Documento é obrigatorio.")]
        public string DocumentNumber { get; set; }

        [DisplayName("Codigo de Barras do Darf")]
        [Required(ErrorMessage = "Campo Codigo de Barras é obrigatorio.")]
        public string BarCode { get; set; }

        [DisplayName("Data de Validade do Darf")]
        [Required(ErrorMessage = "Campo Data de Validade é obrigatorio.")]
        public DateTime? ValidationDate { get; set; }

        [DisplayName("Periodo de Apuração do Darf")]
        [Required(ErrorMessage = "Campo Periodo de Apuração é obrigatorio.")]
        public DateTime PeriodoApuracao { get; set; }

        [DisplayName("Cnpj do Darf")]
        [Required(ErrorMessage = "Campo Cnpj é obrigatorio.")]
        public string Cnpj { get; set; }

        [DisplayName("Codigo da Receita do Darf")]
        [Required(ErrorMessage = "Campo Codigo da Receita é obrigatorio.")]
        public string ReceitaCode { get; set; }

        [DisplayName("Valor Principal do Darf")]
        [Required(ErrorMessage = "Campo Valor Principal é obrigatorio.")]
        public string MainValue { get; set; }

        [DisplayName("Quantidade de Multa do Darf")]
        [Required(ErrorMessage = "Campo Quantidade de Multa é obrigatorio.")]
        public decimal AmountFine { get; set; }

        [DisplayName("Tipo de Pagamento do Darf")]
        [Required(ErrorMessage = "Campo Tipo de Pagamento é obrigatorio.")]
        public bool IsInstallment { get; set; }

        [DisplayName("Taxa de Juros do Darf")]
        [Required(ErrorMessage = "Campo Taxa de Juros é obrigatorio.")]
        public decimal Interest { get; set; }

        [DisplayName("Valor total do Darf")]
        [Required(ErrorMessage = "Campo Valor total é obrigatorio.")]
        public decimal TotalValue { get; set; }

        [DisplayName("Prestador de Serviço")]
        [Required(ErrorMessage = "Campo Prestador de Serviço é obrigatorio.")]
        public string Prestador { get; set; }

    }
}
