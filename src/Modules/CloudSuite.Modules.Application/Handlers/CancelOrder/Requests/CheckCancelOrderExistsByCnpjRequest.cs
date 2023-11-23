using CloudSuite.Modules.Application.Handlers.CancelOrder.Responses;
using CloudSuite.Modules.Common.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.CancelOrder.Requests
{
    public class CheckCancelOrderExistsByCnpjRequest : IRequest<CheckCancelOrderExistsByCnpjResponse>
    {

        public Guid Id { get; set; }

        public Cnpj Cnpj { get; private set; }

        public CheckCancelOrderExistsByCnpjRequest(Guid id, Cnpj cnpj)
        {
            Id = id;
            Cnpj = cnpj;
        }

    }
}
