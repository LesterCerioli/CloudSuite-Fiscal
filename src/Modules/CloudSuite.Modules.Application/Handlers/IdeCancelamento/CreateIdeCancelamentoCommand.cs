using CloudSuite.Modules.Application.Handlers.IdeCancelamento.Responses;
using CloudSuite.Modules.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeCancelamentoEntity = CloudSuite.Modules.Domain.Models.IdeCancelamento;
using CancelOrderEntity = CloudSuite.Modules.Domain.Models.CancelOrder;

namespace CloudSuite.Modules.Application.Handlers.IdeCancelamento
{
    public class CreateIdeCancelamentoCommand : IRequest<CreateIdeCancelamentoResponse>
    {
        public Guid Id { get; private set; }

        public CancelOrderEntity CancelOrder { get; set; }

        public string? CancelReason { get; set; }

        public DateTimeOffset TimeDate { get; set; }

        public CreateIdeCancelamentoCommand()
        {
            Id = Guid.NewGuid();
        }

        public IdeCancelamentoEntity GetEntity()
        {
            return new IdeCancelamentoEntity(
                this.CancelOrder,
                this.CancelReason,
                this.TimeDate
                );
        }
    }
}
