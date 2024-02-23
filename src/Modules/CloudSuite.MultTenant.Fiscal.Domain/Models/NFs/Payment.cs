using NetDevPack.Domain;

namespace CloudSuite.MultTenant.Fiscal.Domain.Models.NFs
{
    public class Payment : Entity, IAggregateRoot
    {
        public decimal? PaymentValue { get; private set; }

        public string? Quote { get; private set; }

        public PaymentMethodEnum PaymentMethodEnum { get; private set; }
        
    }
}