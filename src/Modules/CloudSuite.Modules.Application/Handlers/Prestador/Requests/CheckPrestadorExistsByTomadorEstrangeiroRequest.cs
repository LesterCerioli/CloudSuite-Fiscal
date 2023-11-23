using CloudSuite.Modules.Application.Handlers.Prestador.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Prestador.Requests
{
    public class CheckPrestadorExistsByTomadorEstrangeiroRequest : IRequest<CheckPrestadorExistsByTomadorEstrangeiroResponse>
    {

        public Guid Id { get; private set; }

        public string? DocTomadorEstrangeiro { get; private set; }

        public CheckPrestadorExistsByTomadorEstrangeiroRequest(string? docTomadorEstrangeiro)
        {
            Id = Guid.NewGuid();
            DocTomadorEstrangeiro = docTomadorEstrangeiro;
        }

    }
}
