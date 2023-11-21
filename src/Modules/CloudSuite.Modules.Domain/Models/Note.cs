using CloudSuite.Modules.Common.Enums;
using CloudSuite.Modules.Common.ValueObjects;
using NetDevPack.Domain;

namespace CloudSuite.Modules.Domain.Models
{
    public class Note : Entity, IAggregateRoot
    {
        public Note(TomadorServico tomadorServico, Address address, Country country, 
            District district, Prestador prestador, Cnpj cnpj, string? noteNumber, decimal? value)
        {
            TomadorServico = tomadorServico;
            Address = address;
            Country = country;
            District = district;
            Prestador = prestador;
            Cnpj = cnpj;
            EmissionDate = DateTime.Now;
            NoteNumber = noteNumber;
            Value = value;
        }

        public TomadorServico TomadorServico { get; private set; }

        public Address Address { get; private set; }

        public Country Country { get; private set; }

        public District District { get; private set; }

        public Prestador Prestador { get; private set; }

        public Cnpj Cnpj { get; private set; }

        public string? NoteNumber { get; private set; }

        public DateTime? EmissionDate  { get; private set; }

        public decimal? Value { get; private set; }

    }
}