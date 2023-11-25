using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Requests
{
    public class CheckDeclaracaoIRExistsByAlimonyRequest : IRequest<CheckDeclaracaoIRExistsByAlimonyResponse>
    {
       
        public Guid Id { get; private set; }

        public decimal Alimony { get; set; }

        public CheckDeclaracaoIRExistsByAlimonyRequest(decimal alimony)
        {
            Id = Guid.NewGuid();
            Alimony = alimony;
        }
    }
}

