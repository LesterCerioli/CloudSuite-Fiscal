﻿using CloudSuite.Modules.Application.Handlers.CancelOrder.Responses;
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

        public Guid Id { get; private set; }

        public Cnpj Cnpj { get; set; }

        public CheckCancelOrderExistsByCnpjRequest(Cnpj cnpj)
        {
            Id = Guid.NewGuid();
            Cnpj = cnpj;
        }

    }
}
