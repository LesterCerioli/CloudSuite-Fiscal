using CloudSuite.Modules.Application.Handlers.State.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.State.Requests
{
    public class CheckStateExistsByUfRequest : IRequest<CheckStateExistsByUfResponse>
    {

        public Guid Id { get; private set; }

        public string? UF { get; private set; }

        public CheckStateExistsByUfRequest(Guid id, string? uF)
        {
            Id = Guid.NewGuid();
            UF = uF;
        }
    }
}
