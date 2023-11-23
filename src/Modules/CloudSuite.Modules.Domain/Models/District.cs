using System.ComponentModel.DataAnnotations;
using NetDevPack.Domain;

namespace CloudSuite.Modules.Domain.Models
{
    public class District : Entity, IAggregateRoot
    {
        
        private readonly List<State> _states;
        public District(Guid id, string name, string type, string location)
        {
            Id = Guid.NewGuid();
            _states = new List<State>(0);
            Name = name;
            Type = type;
            Location = location;
        }

        public District() { }

        public IReadOnlyCollection<State> States => _states.AsReadOnly();

        public State State { get; set; }

        public Guid StateId { get; private set; }

        public string? Name { get; private set; }

        public string? Type {  get; private set; }

        public string? Location { get; private set; }
        
        
    }
}