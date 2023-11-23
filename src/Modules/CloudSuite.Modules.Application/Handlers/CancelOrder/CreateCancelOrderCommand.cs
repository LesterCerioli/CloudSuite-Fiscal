using CloudSuite.Modules.Application.Handlers.CancelOrder.Responses;
using CloudSuite.Modules.Common.ValueObjects;
using MediatR;
using IdeCancelamentoEntity = CloudSuite.Modules.Domain.Models.IdeCancelamento;
using CancelOrderEntity = CloudSuite.Modules.Domain.Models.CancelOrder;

namespace CloudSuite.Modules.Application.Handlers.CancelOrder
{
    public class CreateCancelOrderCommand : IRequest<CreateCancelOrderResponse>
    {
        public Guid Id { get; private set; }

        public IdeCancelamentoEntity IdeCancelamento { get; private set; }

        public DateTimeOffset RequestDate { get; private set; }

        public Cnpj Cnpj { get; private set; }


        public CancelOrderEntity GetEntity()
        {
            return new CancelOrderEntity(
                this.IdeCancelamento,
                this.RequestDate,
                this.Cnpj
                );
        }
    }
}
