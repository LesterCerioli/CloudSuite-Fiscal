using CloudSuite.Modules.Common.ValueObjects;
using NetDevPack.Domain;

namespace CloudSuite.Modules.Domain.Models
{
    public class Darf : Entity, IAggregateRoot
    {
        public Darf(string? referenceMonth, DateTime? dueDate, 
            string? referenceYear, string? paymentValue, 
            string? recuboDeclaroNumero, string? documentNumber,
            string barCode)
        {
            ReferenceMonth = referenceMonth;
            DueDate = dueDate;
            ReferenceYear = referenceYear;
            PaymentValue = paymentValue;
            RecuboDeclaroNumero = recuboDeclaroNumero;
            DocumentNumber = documentNumber;
            BarCode = barCode;
        }

        public string? ReferenceMonth { get; private set; }

        public DateTime? DueDate { get; private set; }

        public string? ReferenceYear { get; private set; }

        public decimal? PaymentValue { get; private set; }

        public string? RecuboDeclaroNumero { get; private set; }

        public string? DocumentNumber { get; private set; }

        public string? BarCode { get; private set; }

        public DateTime? ValidationDate { get; private set; }

        public DateTime? PeriodoApuracao { get; private set; }

        public Cnpj Cnpj { get; private set; }

        public string? ReceitaCode { get; private set; }

        public string? MainValue { get; private set; }

        //Valor de Multa
        public decimal? AmountFine  { get; private set; }

        //Juros
        public decimal? Interest { get; private set; }

        public decimal? TotalValue { get; private set; }



        
    }
}