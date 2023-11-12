using System.ComponentModel.DataAnnotations;
using NetDevPack.Domain;

namespace CloudSuite.Modules.Domain.Models
{
    public class City : Entity, IAggregateRoot
    {
        private readonly List<State> _states;

        public City(Guid id, string? cityName, State state)
        {
            StateId = id;
            _states = new List<State>();
            CityName = cityName;
            State = state;
        }

        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [MaxLength(100)]

        public string? CityName { get; private set; }

        public IReadOnlyCollection<State> States => _states.AsReadOnly();

        public State State { get; private set; }

        public Guid StateId { get; private set; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}