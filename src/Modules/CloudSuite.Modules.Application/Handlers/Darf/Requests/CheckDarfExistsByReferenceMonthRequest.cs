using CloudSuite.Modules.Application.Handlers.Darf.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Darf.Requests
{
    public class CheckDarfExistsByReferenceMonthRequest : IRequest<CheckDarfExistsByReferenceMonthResponse>
    {
        public Guid Id { get; private set; }

        public string? ReferenceMonth { get; set; }

        public CheckDarfExistsByReferenceMonthRequest(string referenceMonth)
        {
            Id = Guid.NewGuid();
            ReferenceMonth = referenceMonth;
        }
    }
}
