using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSuite.Modules.Domain.Models;

namespace CloudSuite.Modules.Application.ViewModels
{
    public class PessoaViewModel
    {
        [Key]
        public Guid Id { get; private set; }

        [DisplayName("Nome da Pessoa")]
        [Required(ErrorMessage = "A idade é obrigatoria.")]
        [StringLength(30)]
        public string? Nome { get; set; }

        [DisplayName("Nome de Contato")]
        [Required(ErrorMessage = "A idade é obrigatoria.")]
        [StringLength(3)]
        public string? Idade { get; set; }
    }
}
