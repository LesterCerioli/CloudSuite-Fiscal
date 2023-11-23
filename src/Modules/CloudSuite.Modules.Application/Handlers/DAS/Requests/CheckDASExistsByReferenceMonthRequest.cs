using CloudSuite.Modules.Application.Handlers.DAS.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.DAS.Requests
{
    public class CheckDASExistsByReferenceMonthRequest : IRequest<CheckDASExistsByReferenceMonthResponse>
    {

        public Guid Id { get; set; }

        public string ReferenceMonth { get; private set; }

        public CheckDASExistsByReferenceMonthRequest(Guid id, string? referenceMonth)
        {
            Id = Guid.NewGuid();
            ReferenceMonth = referenceMonth;
        }
    }
}
