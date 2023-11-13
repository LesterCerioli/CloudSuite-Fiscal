using NetDevPack.Domain;

namespace CloudSuite.Modules.Domain.Models
{
    public sealed class IdeCancelamento : Entity, IAggregateRoot
    {
        public IdeCancelamento(CancelOrder cancelOrder, string? cancelReason, DateTimeOffset? timeDate)
        {
            CancelOrder = cancelOrder;
            CancelReason = cancelReason;
            TimeDate = DateTimeOffset.Now;
        }

        public CancelOrder CancelOrder { get; private set; }

        public string? CancelReason { get; private set; }

        public DateTimeOffset? TimeDate { get; private set; }

        //public DFeSignature Signature { get; internal set; }

        
    }
}