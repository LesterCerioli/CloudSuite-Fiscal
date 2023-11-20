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
    public class CheckDeclaracaoIRExistsByCpfRequest : IRequest<CheckDeclaracaoIRExistsByCpfResponse>
    {

        public Guid Id { get; private set; }

        public Cpf Cpf { get; private set; }

        public CheckDeclaracaoIRExistsByCpfRequest(Guid id, Cpf cpf)
        {
            Id = id;
            Cpf = cpf;
        }
    }
}
