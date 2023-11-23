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

        public Cnpj Cnpj { get; set; }

        public string? InscricaoMunicipal { get; set; }

        public string? InscricaoEstadual { get; set; }

        public string? DocTomadorEstrangeiro { get; set; }

        public string? SocialReason { get; set; }

        public string NomeFantasia { get; set; }

        public AdressEntity Address { get; set; }

        public int Tipo { get; set; }

        public CreatePrestadorCommand()
        {
            Id = Guid.NewGuid();
        }

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
