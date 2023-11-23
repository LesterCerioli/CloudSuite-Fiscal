using CloudSuite.Modules.Application.Handlers.Darf.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Darf.Requests
{
    public class CheckDarfExistsByDueDateRequest : IRequest<CheckDarfExistsByDueDateResponse>
    {

        public Guid Id { get; set; }

        public DateTime DueDate { get; private set; }

        public CheckDarfExistsByDueDateRequest(Guid id, DateTime dueDate)
        {
            Id = Guid.NewGuid();
            DueDate = dueDate;
        }
    }
}
