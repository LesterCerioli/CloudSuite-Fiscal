using CloudSuite.Modules.Application.Handlers.CancelOrder.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.CancelOrder.Requests
{
    public class CheckCancelOrderExistsByRequestDateRequest : IRequest<CheckCancelOrderExistsByRequestDateResponse>
    {

        public Guid Id { get; set; }

        public DateTimeOffset RequestDate { get; private set; }

        public CheckCancelOrderExistsByRequestDateRequest(Guid id, DateTimeOffset requestDate)
        {
            Id = Guid.NewGuid();
            RequestDate = requestDate;
        }
    }
}
