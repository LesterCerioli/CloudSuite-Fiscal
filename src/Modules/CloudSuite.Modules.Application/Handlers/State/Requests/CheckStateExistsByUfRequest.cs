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

        public string? UF { get; set; }

        public CheckStateExistsByUfRequest(string? uf)
        {
            Id = Guid.NewGuid();
            UF = uf;
        }
    }
}
