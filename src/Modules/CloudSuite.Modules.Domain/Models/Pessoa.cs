using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Domain.Models
{
    public class Pessoa : Entity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string? Nome { get; private set; }

        public string? Idade { get; private set; }

        public Pessoa(Guid id, string? nome, string? idade)
        {
            Id = id;
            Nome = nome;
            Idade = idade;
        }
    }
}
