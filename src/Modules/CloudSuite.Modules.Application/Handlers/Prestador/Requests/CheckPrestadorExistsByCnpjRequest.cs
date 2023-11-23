using CloudSuite.Modules.Application.Handlers.Prestador.Responses;
using CloudSuite.Modules.Common.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Prestador.Requests
{
    public class CheckPrestadorExistsByCnpjRequest : IRequest<CheckPrestadorExistsByCnpjResponse>
    {

        public Guid Id { get; private set; }

        public Cnpj Cnpj { get; private set; }

        public CheckPrestadorExistsByCnpjRequest(Cnpj cnpj)
        {
            Id = Guid.NewGuid();
            Cnpj = cnpj;
        }
    }
}
