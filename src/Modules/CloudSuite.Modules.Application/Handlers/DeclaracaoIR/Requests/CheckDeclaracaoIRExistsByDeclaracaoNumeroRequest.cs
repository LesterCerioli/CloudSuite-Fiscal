using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Requests
{
    public class CheckDeclaracaoIRExistsByDeclaracaoNumeroRequest : IRequest<CheckDeclaracaoIRExistsByDeclaracaoNumeroResponse>
    {
        public Guid Id { get; private set; }

        public string? DeclaracoaNumero { get; set; }

        public CheckDeclaracaoIRExistsByDeclaracaoNumeroRequest(string? declaracoaNumero)
        {
            Id = Guid.NewGuid();
            DeclaracoaNumero = declaracoaNumero;
        }
    }
}
