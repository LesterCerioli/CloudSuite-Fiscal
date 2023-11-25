﻿using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using CloudSuite.Modules.Common.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Requests
{
    public class CheckDeclaracaoIRExistsByCnpjRequest : IRequest<CheckDeclaracaoIRExistsByCnpjResponse>
    {
        
        public Guid Id { get; private set; }

        public Cnpj Cnpj { get; set; }

        public CheckDeclaracaoIRExistsByCnpjRequest(Cnpj cnpj)
        {
            Id = Guid.NewGuid();
            Cnpj = cnpj;
        }
    }
}
