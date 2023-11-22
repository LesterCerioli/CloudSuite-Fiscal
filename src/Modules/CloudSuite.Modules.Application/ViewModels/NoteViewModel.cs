using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.ViewModels
{
    public class NoteViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Numero da Nota")]
        [Required(ErrorMessage = "The field is required.")]
        public string NoteNumber { get; set; }

        [DisplayName("Data de Emissão da Nota")]
        [Required(ErrorMessage = "The field is required.")]
        public DateTime EmissionDate { get; set; }

        [DisplayName("Valor")]
        [Required(ErrorMessage = "The field is required.")]
        public decimal Value { get; set; }
    }
}
