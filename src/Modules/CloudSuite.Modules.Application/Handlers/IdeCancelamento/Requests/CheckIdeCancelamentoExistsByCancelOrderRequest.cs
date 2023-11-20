using CloudSuite.Modules.Application.Handlers.IdeCancelamento.Responses;
using CloudSuite.Modules.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.IdeCancelamento.Requests
{
    public class CheckIdeCancelamentoExistsByCancelOrderRequest : IRequest<CheckIdeCancelamentoExistsByCancelOrderResponse>
    {
        public Guid Id { get; private set; }

        public CancelOrder CancelOrder { get; private set; }

        public CheckIdeCancelamentoExistsByCancelOrderRequest(Guid id, CancelOrder cancelOrder)
        {
            Id = id;
            CancelOrder = cancelOrder;
        }
    }
}
