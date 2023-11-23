using CloudSuite.Modules.Application.Handlers.IdeCancelamento.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.IdeCancelamento.Requests
{
    public class CheckIdeCancelamentoExistsByTimeDateRequest : IRequest<CheckIdeCancelamentoExistsByTimeDateResponse>
    {

        public Guid Id { get; private set; }

        public DateTimeOffset TimeDate { get; private set; }

        public CheckIdeCancelamentoExistsByTimeDateRequest(DateTimeOffset timeDate)
        {
            Id = Guid.NewGuid();
            TimeDate = timeDate;
        }
    }
}
