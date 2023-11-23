using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Requests
{
    public class CheckDeclaracaoIRExistsByTotalIncomeRequest : IRequest<CheckDeclaracaoIRExistsByTotalIncomeResponse>
    {

        public Guid Id { get; private set; }

        public decimal TotalIncome { get; private set; }

        public CheckDeclaracaoIRExistsByTotalIncomeRequest(decimal totalIncome)
        {
            Id = Guid.NewGuid();
            TotalIncome = totalIncome;
        }

    }
}
