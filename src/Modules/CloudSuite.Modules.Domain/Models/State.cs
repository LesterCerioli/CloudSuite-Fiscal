using System.ComponentModel.DataAnnotations;
using NetDevPack.Domain;


namespace CloudSuite.Modules.Domain.Models
{
    public class State : Entity, IAggregateRoot
    {
        
        private readonly List<Country> _countries;

        public State(Guid id, string uf, string stateName, Country country, Guid countryId)
        {
            Id = Guid.NewGuid();
            _countries = new List<Country>();
            UF = uf;
            StateName = stateName;
            Country = country;
            CountryId = countryId;
        }

        [Required(ErrorMessage="Este campo � de preenchimento obrigat�rio.")]
        [StringLength(100)]
        public string? StateName { get; private set; }

        [Required(ErrorMessage = "Este cmapo � de preenchimento obrigat�rio.")]
        public string? UF { get; private set; }

        public Country Country { get; private set; }

        public Guid CountryId { get; private set; }

        public IReadOnlyCollection<Country> Countries => _countries.AsReadOnly();
        
        
    }
}