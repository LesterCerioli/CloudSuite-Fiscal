using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
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

        public Cnpj Cnpj { get; private set; }

        public CheckDeclaracaoIRExistsByCnpjRequest(Guid id, Cnpj cnpj)
        {
            Id = id;
            Cnpj = cnpj;
        }
    }
}
