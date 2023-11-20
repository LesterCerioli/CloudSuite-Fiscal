using CloudSuite.Modules.Application.Handlers.TomadorServico.Responses;
using CloudSuite.Modules.Common.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressEntity = CloudSuite.Modules.Domain.Models.Address;
using TomadorServicoEntity = CloudSuite.Modules.Domain.Models.TomadorServico;

namespace CloudSuite.Modules.Application.Handlers.TomadorServico
{
    public class CreateTomadorServicoCommand : IRequest<CreateTomadorServicoResponse>
    {
        public Guid Id { get; private set; }

        public Cnpj Cnpj { get; private set; }

        public string? InscricaoMunicipal { get; private set; }

        public string? InscricaoEstadual { get; private set; }

        public string? DocTomadorEstrangeiro { get; private set; }

        public string? SocialReason { get; private set; }

        public string NomeFantasia { get; set; }

        public AddressEntity Address { get; private set; }

        public int Tipo { get; set; }

        public TomadorServicoEntity GetEntity()
        {
            return new TomadorServicoEntity(
                this.Cnpj,
                this.InscricaoMunicipal,
                this.InscricaoEstadual,
                this.DocTomadorEstrangeiro,
                this.SocialReason,
                this.NomeFantasia,
                this.Address,
                this.Tipo
                );
        }
    }
}
