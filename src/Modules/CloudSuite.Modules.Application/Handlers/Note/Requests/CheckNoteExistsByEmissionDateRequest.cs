using CloudSuite.Modules.Application.Handlers.Note.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Note.Requests
{
    public class CheckNoteExistsByEmissionDateRequest : IRequest<CheckNoteExistsByEmissionDateResponse>
    {

        public Guid Id { get; set; }

        public DateTime? EmissionDate { get; private set; }

        public CheckNoteExistsByEmissionDateRequest(DateTime? emissionDate)
        {
            Id = Guid.NewGuid();
            EmissionDate = emissionDate;
        }
    }
}
