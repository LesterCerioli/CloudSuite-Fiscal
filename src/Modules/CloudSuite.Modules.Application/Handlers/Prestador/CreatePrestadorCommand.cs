using CloudSuite.Modules.Application.Handlers.Prestador.Responses;
using CloudSuite.Modules.Common.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdressEntity = CloudSuite.Modules.Domain.Models.Address;
using PrestadorEntity = CloudSuite.Modules.Domain.Models.Prestador;

namespace CloudSuite.Modules.Application.Handlers.Prestador
{
    public class CreatePrestadorCommand : IRequest<CreatePrestadorResponse>
    {
        public Guid Id { get; private set; }

        public Cnpj Cnpj { get; private set; }

        public string? InscricaoMunicipal { get; private set; }

        public string? InscricaoEstadual { get; private set; }

        public string? DocTomadorEstrangeiro { get; private set; }

        public string? SocialReason { get; private set; }

        public string NomeFantasia { get; set; }

        public AdressEntity Address { get; private set; }

        public int Tipo { get; set; }

        public PrestadorEntity GetEntity()
        {
            return new PrestadorEntity(
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
