using CloudSuite.Modules.Application.Handlers.Darf.Responses;
using CloudSuite.Modules.Application.Handlers.DAS.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.DAS.Requests
{
    public class CheckDASExistsByDocumentNumberRequest : IRequest<CheckDASExistsByDocumentNumberResponse>
    {

        public Guid Id { get; set; }

        public string DocumentNumber { get; private set; }

        public CheckDASExistsByDocumentNumberRequest(Guid id, string documentNumber)
        {
            Id = Guid.NewGuid();
            DocumentNumber = documentNumber;
        }
    }
}
