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
        public Guid Id { get; private set; }

        [DisplayName("Mês de Referencia do Darf")]
        public string? ReferenceMonth { get; private set; }

        [DisplayName("Data de Vencimento do Darf")]
        public DateTime? DueDate { get; private set; }

        [DisplayName("Ano de Referencia do Darf")]
        public string? ReferenceYear { get; private set; }

        [DisplayName("Valor de Pagamento no Darf")]
        public decimal? DarfPaymentValue { get; private set; }

        [DisplayName("Recibo Declarado no Darf")]
        public string? RecuboDeclaroNumero { get; private set; }

        [DisplayName("Numero do Documento no Darf")]
        public string? DocumentNumber { get; private set; }

        [DisplayName("Codigo de Barras do Darf")]
        public string? BarCode { get; private set; }

        [DisplayName("Data de Validade do Darf")]
        public DateTime? ValidationDate { get; private set; }

        [DisplayName("Perido de Apuração do Darf")]
        public DateTime? PeriodoApuracao { get; private set; }

        [DisplayName("Cnpj no Darf")]
        public Cnpj Cnpj { get; private set; }

        [DisplayName("Codigo da Receito do Darf")]
        public string? ReceitaCode { get; private set; }

        [DisplayName("Valor Principal do Darf")]
        public string? MainValue { get; private set; }

        [DisplayName("Quantidade de Multa do Darf")]
        public decimal? AmountFine { get; private set; }

        [DisplayName("Tipo de Pagamento do Darf")]
        public bool? IsInstallment { get; private set; }

        [DisplayName("Taxa de Juros do Darf")]
        public decimal? Interest { get; private set; }

        [DisplayName("Valor total do Darf")]
        public decimal? TotalValue { get; private set; }

        [DisplayName("Prestador de Serviço do Darf")]
        public Prestador Prestador { get; private set; }
    }
}
