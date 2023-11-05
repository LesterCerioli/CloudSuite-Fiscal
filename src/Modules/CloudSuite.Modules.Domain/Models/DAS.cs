using NetDevPack.Domain;

namespace CloudSuite.Modules.Domain.Models
{
    public class DAS : Entity, IAggregateRoot
    {
        public DAS(string? referenceMonth, DateTime? dueDate, 
        string? referenceYear, string? paymentValue, 
        string? documentNumber, string barCode)
        {
            ReferenceMonth = referenceMonth;
            DueDate = dueDate;
            ReferenceYear = referenceYear;
            PaymentValue = paymentValue;
            DocumentNumber = documentNumber;
            BarCode = barCode;
        }

        public string? ReferenceMonth { get; private set; }

        public DateTime? DueDate { get; private set; }

        public string? ReferenceYear { get; private set; }

        public string? PaymentValue { get; private set; }

        public string? DocumentNumber { get; private set; }

        public string? BarCode { get; private set; }

        
    }
}