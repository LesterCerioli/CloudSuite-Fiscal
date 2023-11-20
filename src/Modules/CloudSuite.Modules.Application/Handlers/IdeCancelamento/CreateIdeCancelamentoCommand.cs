using CloudSuite.Modules.Application.Handlers.IdeCancelamento.Responses;
using CloudSuite.Modules.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeCancelamentoEntity = CloudSuite.Modules.Domain.Models.IdeCancelamento;

namespace CloudSuite.Modules.Application.Handlers.IdeCancelamento
{
    public class CreateIdeCancelamentoCommand : IRequest<CreateIdeCancelamentoResponse>
    {
        public Guid Id { get; private set; }

        public CancelOrder CancelOrder { get; private set; }

        public string? CancelReason { get; private set; }

        public DateTimeOffset? TimeDate { get; private set; }

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
