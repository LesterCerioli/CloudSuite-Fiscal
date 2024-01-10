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
    public class TomadorServicoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Cnpj")]
        [Required(ErrorMessage = "Campo Cnpj é obrigatorio.")]
        public string Cnpj { get; set; }

        [DisplayName("Inscricao Municipal")]
        [Required(ErrorMessage = "Campo Inscricao Municipal é obrigatorio.")]
        public string InscricaoMunicipal { get; set; }

        [DisplayName("Inscricao Estadual")]
        [Required(ErrorMessage = "Campo Inscricao Estadual é obrigatorio.")]
        public string InscricaoEstadual { get; set; }

        [DisplayName("Documento Estrangeiro")]
        [Required(ErrorMessage = "Campo Documento Estrangeiro é obrigatorio.")]
        public string DocTomadorEstrangeiro { get; set; }

        [DisplayName("Razão Social")]
        [Required(ErrorMessage = "Campo Razão Social é obrigatorio.")]
        public string SocialReason { get; set; }

        [DisplayName("Nome Fantasia")]
        [Required(ErrorMessage = "Campo Nome Fantasia é obrigatorio.")]
        public string NomeFantasia { get; set; }

        [DisplayName("Tipo")]
        [Required(ErrorMessage = "Campo Tipo é obrigatorio.")]
        public int Tipo { get; set; }
    }
}
