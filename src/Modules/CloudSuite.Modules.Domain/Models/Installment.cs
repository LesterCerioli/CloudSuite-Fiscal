using System.ComponentModel.DataAnnotations;
using NetDevPack.Domain;

namespace CloudSuite.Modules.Domain.Models
{
    public class Installment : Entity, IAggregateRoot
    {
        public Installment(Guid id, DateTime? dueDate, decimal? value, string? description)
        {
            Id = Guid.NewGuid();
            DueDate = dueDate;
            Value = value;
            Description = description;
        }

        [Required(ErrorMessage = = "Preenchimento Obrigatório")]
        public DateTime? DueDate { get; private set; }

        [Required(ErrorMessage = = "Preenchimento Obrigatório")]
        public decimal? Value { get; private set; }

        [Required(ErrorMessage = = "Preenchimento Obrigatório")]
        public string? Description { get; private set; }
    }
}