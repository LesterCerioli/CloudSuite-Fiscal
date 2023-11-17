using CloudSuite.Modules.Application.Handlers.DAS.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.DAS.Requests
{
    public class CheckDASExistsByDueDateRequest : IRequest<CheckDASExistsByDueDateResponse>
    {
        
        public Guid Id { get; set; }

        public DateTime DueDate { get; private set; }

        public CheckDASExistsByDueDateRequest(Guid id, DateTime dueDate)
        {
            Id = Guid.NewGuid();
            DueDate = dueDate;
        }
    }
}
