using CloudSuite.Modules.Application.Handlers.IdeCancelamento.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.IdeCancelamento.Requests
{
    public class CheckIdeCancelamentoExistsByCancelReasonRequest : IRequest<CheckIdeCancelamentoExistsByCancelReasonResponse>
    {
        
        public Guid Id { get; private set; }

        public string? CancelReason { get; set; }

        public CheckIdeCancelamentoExistsByCancelReasonRequest(string? cancelReason)
        {
            Id = Guid.NewGuid();
            CancelReason = cancelReason;
        }
    }
}
