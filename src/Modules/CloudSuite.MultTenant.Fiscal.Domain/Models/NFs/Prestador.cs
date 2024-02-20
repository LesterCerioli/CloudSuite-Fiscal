using CloudSuite.MultTenant.Fiscal.Domain.ValueObjects;
using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.MultTenant.Fiscal.Domain.Models.NFs
{
    public class Prestador : Entity, IAggregateRoot
    {
        public Cnpj Cnpj { get; private set; }

        public string? InscricaoMunicipal { get; private set; }

        public string? NumeroEmissorRps { get; private set; }

        public string? RazaoSocial { get; private set; }

        public string? NomeFantasia { get; private set; }

        public Address Address { get; private set; }
    }
}
