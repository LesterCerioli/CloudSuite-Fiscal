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
    public class IdeNFSeViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Numero do NFSe")]
        [Required(ErrorMessage = "Campo Number é obrigatorio.")]
        public string Number { get; set; }

        [DisplayName("Chave do NFSe")]
        [Required(ErrorMessage = "Campo Key é obrigatorio.")]
        public string Key { get; set; }

        [DisplayName("Data de Emissão")]
        [Required(ErrorMessage = "Campo EmissionDate é obrigatorio.")]
        public DateTime EmissionDate { get; set; }

        [DisplayName("Data de Emissão")]
        [Required(ErrorMessage = "Campo Note é obrigatorio.")]
        public string Note { get; set; }

    }
}
