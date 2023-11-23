using CloudSuite.Modules.Application.Handlers.DAS.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.DAS.Requests
{
    public class CheckDASExistsByReferenceYearRequest : IRequest<CheckDASExistsByReferenceYearResponse>
    {

        public Guid Id { get; set; }

        public string ReferenceYear { get; private set; }

        public CheckDASExistsByReferenceYearRequest(Guid id, string referenceYear)
        {
            Id = Guid.NewGuid();
            ReferenceYear = referenceYear;
        }
    }
}
