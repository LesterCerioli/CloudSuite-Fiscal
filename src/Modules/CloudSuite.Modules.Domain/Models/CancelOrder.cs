using CloudSuite.Modules.Common.ValueObjects;
using NetDevPack.Domain;

namespace CloudSuite.Modules.Domain.Models
{
    public class CancelOrder : Entity, IAggregateRoot
    {
		public CancelOrder(IdeCancelamento ideCancelamento, DateTimeOffset? requestDate, Cnpj cnpj)
		{
			IdeCancelamento = ideCancelamento;
			RequestDate = DateTimeOffset.UtcNow;
			Cnpj = cnpj;
		}

		public IdeCancelamento IdeCancelamento { get; private set; }

        public DateTimeOffset? RequestDate { get; private set; }

        public Cnpj Cnpj { get; private set; }

    }
}