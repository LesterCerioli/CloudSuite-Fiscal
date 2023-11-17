using CloudSuite.Modules.Common.Enums;
using CloudSuite.Modules.Common.ValueObjects;
using NetDevPack.Domain;

namespace CloudSuite.Modules.Domain.Models
{
    public class Note : Entity, IAggregateRoot
    {
        public Note(TomadorServico tomadorServico, Address address, Country country, District district, Prestador prestador)
        {
            TomadorServico = tomadorServico;
            Address = address;
            Country = country;
            District = district;
            Prestador = prestador;
        }

        public TomadorServico TomadorServico { get; private set; }

        public Address Address { get; private set; }

        public Country Country { get; private set; }

        public District District { get; private set; }

        public Prestador Prestador { get; private set; }
        
    }
}