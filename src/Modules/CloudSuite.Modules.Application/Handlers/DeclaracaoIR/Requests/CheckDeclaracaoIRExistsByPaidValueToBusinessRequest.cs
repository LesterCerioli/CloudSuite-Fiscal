using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Requests
{
    public class CheckDeclaracaoIRExistsByPaidValueToBusinessRequest : IRequest<CheckDeclaracaoIRExistsByPaidValueToBusinessResponse>
    {
        
        public Guid Id { get; private set; }

        public decimal? PaidValueToBusiness { get; private set; }

        public CheckDeclaracaoIRExistsByPaidValueToBusinessRequest(Guid id, decimal? paidValueToBusiness)
        {
            Id = id;
            PaidValueToBusiness = paidValueToBusiness;
        }
    }
}
