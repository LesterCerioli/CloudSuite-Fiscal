using CloudSuite.Modules.Application.Handlers.TomadorServico.Responses;
using CloudSuite.Modules.Common.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.TomadorServico.Requests
{
    public class CheckTomadorServicoByCnpjRequest : IRequest<CheckTomadorServicoByCnpjResponse>
    {

        public Guid Id { get; private set; }

        public Cnpj Cnpj { get; private set; }

        public CheckTomadorServicoByCnpjRequest(Guid id, Cnpj cnpj)
        {
            Id = Guid.NewGuid();
            Cnpj = cnpj;
        }
    }
}
