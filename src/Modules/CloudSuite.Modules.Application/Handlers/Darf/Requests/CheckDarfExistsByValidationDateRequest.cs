using CloudSuite.Modules.Application.Handlers.Darf.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Darf.Requests
{
    public class CheckDarfExistsByValidationDateRequest : IRequest<CheckDarfExistsByValidationDateResponse>
    {

        public Guid Id { get; set; }

        public DateTime ValidationDate { get; private set; }

        public CheckDarfExistsByValidationDateRequest(Guid id, DateTime validationDate)
        {
            Id = Guid.NewGuid();
            ValidationDate = validationDate;
        }
    }
}
