using CloudSuite.Modules.Common.ValueObjects;
using NetDevPack.Domain;

namespace CloudSuite.Modules.Domain.Models
{
    public class Darf : Entity, IAggregateRoot
    {
        public Darf(string? referenceMonth, DateTime dueDate, 
            string? referenceYear, decimal? darfPaymentValue, 
            string? recuboDeclaroNumero, string? documentNumber, 
            string? barCode, DateTime? validationDate, 
            DateTime? periodoApuracao, Cnpj cnpj, 
            string? receitaCode, string? mainValue, decimal? amountFine, 
            decimal? interest, decimal? totalValue)
        {
            ReferenceMonth = referenceMonth;
            DueDate = DateTime.Now;
            ReferenceYear = referenceYear;
            DarfPaymentValue = darfPaymentValue;
            RecuboDeclaroNumero = recuboDeclaroNumero;
            DocumentNumber = documentNumber;
            BarCode = barCode;
            ValidationDate = DateTime.Now;
            PeriodoApuracao = periodoApuracao;
            Cnpj = cnpj;
            ReceitaCode = receitaCode;
            MainValue = mainValue;
            AmountFine = amountFine;
            Interest = interest;
            TotalValue = totalValue;
            
        }

        public string? ReferenceMonth { get; private set; }

        public DateTime? DueDate { get; private set; }

        public string? ReferenceYear { get; private set; }

        public decimal? DarfPaymentValue { get; private set; }

        public string? RecuboDeclaroNumero { get; private set; }

        public string? DocumentNumber { get; private set; }

        public string? BarCode { get; private set; }

        public DateTime? ValidationDate { get; private set; }

        public DateTime? PeriodoApuracao { get; private set; }

        public Cnpj Cnpj { get; private set; }

        public string? ReceitaCode { get; private set; }

        public string? MainValue { get; private set; }

        public decimal? AmountFine  { get; private set; }

        public bool? IsInstallment { get; private set; }
        
        public decimal? Interest { get; private set; }

        public decimal? TotalValue { get; private set; }

        public Prestador Prestador { get; private set; }

    }
}