﻿using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Requests
{
    public class CheckDeclaracaoIRExistsByProfitsDividendsRequest : IRequest<CheckDeclaracaoIRExistsByProfitsDividendsResponse>
    {

        public Guid Id { get; private set; }

        public decimal ProfitsDividends { get; set; }

        public CheckDeclaracaoIRExistsByProfitsDividendsRequest(decimal profitsDividends)
        {
            Id = Guid.NewGuid();
            ProfitsDividends = profitsDividends;
        }
    }
}
