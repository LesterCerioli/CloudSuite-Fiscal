using CloudSuite.Modules.Common.ValueObjects;
using NetDevPack.Domain;

namespace CloudSuite.Modules.Domain.Models
{
    public class Prestador : Entity, IAggregateRoot
    {
        public Prestador(Cnpj cnpj, string? inscricaoMunicipal, string? inscricaoEstadual, string? docTomadorEstrangeiro, string? socialReason, string nomeFantasia, Address address, int tipo)
        {
            Cnpj = cnpj;
            InscricaoMunicipal = inscricaoMunicipal;
            InscricaoEstadual = inscricaoEstadual;
            DocTomadorEstrangeiro = docTomadorEstrangeiro;
            SocialReason = socialReason;
            NomeFantasia = nomeFantasia;
            Address = address;
            Tipo = tipo;
        }

        public Cnpj Cnpj { get; private set; }

        public string? InscricaoMunicipal { get; private set; }

        public string? InscricaoEstadual { get; private set; }

        public string? DocTomadorEstrangeiro { get; private set; }

        public string? SocialReason { get; private set; }

        public string NomeFantasia { get; set; }

        public Address Address { get; private set;}

        public int Tipo { get; set; } //Utilize a classe TipoTomador
        
        //public EnderecoExterior EnderecoExterior { get; }

        //public DadosContato DadosContato { get; }
    }
}