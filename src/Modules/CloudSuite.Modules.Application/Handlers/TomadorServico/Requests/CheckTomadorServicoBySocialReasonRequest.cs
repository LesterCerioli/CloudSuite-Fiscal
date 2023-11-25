using CloudSuite.Modules.Application.Handlers.TomadorServico.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.TomadorServico.Requests
{
    public class CheckTomadorServicoBySocialReasonRequest : IRequest<CheckTomadorServicoBySocialReasonResponse>
    {

        public Guid Id { get; private set; }

        public string? SocialReason { get; set; }

        public CheckTomadorServicoBySocialReasonRequest(string? socialReason)
        {
            Id = Guid.NewGuid();
            SocialReason = socialReason;
        }

    }
}
