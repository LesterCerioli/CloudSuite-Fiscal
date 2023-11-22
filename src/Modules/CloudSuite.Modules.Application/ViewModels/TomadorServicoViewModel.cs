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
        public Guid Id { get; private set; }

        [DisplayName("Cnpj")]
        [Required(ErrorMessage = "Campo Cnpj é obrigatorio.")]
        public string Cnpj { get; set; }

        [DisplayName("Inscricao Municipal do Tomador de Serviço")]
        [Required(ErrorMessage = "Campo InscricaoMunicipal é obrigatorio.")]
        public string InscricaoMunicipal { get; set; }

        [DisplayName("Inscricao Estadual do Tomador de Serviço")]
        [Required(ErrorMessage = "Campo InscricaoEstadual é obrigatorio.")]
        public string InscricaoEstadual { get; set; }

        [DisplayName("Documento Estrangeiro do Tomador de Serviço")]
        [Required(ErrorMessage = "Campo DocTomadorEstrangeiro é obrigatorio.")]
        public string DocTomadorEstrangeiro { get; set; }

        [DisplayName("Razão Social do Tomador de Serviço")]
        [Required(ErrorMessage = "Campo SocialReason é obrigatorio.")]
        public string SocialReason { get; set; }

        [DisplayName("Nome Fantasia do Tomador de Serviço")]
        [Required(ErrorMessage = "Campo NomeFantasia é obrigatorio.")]
        public string NomeFantasia { get; set; }

        [DisplayName("Tipo do Tomador de Serviço")]
        [Required(ErrorMessage = "Campo Tipo é obrigatorio.")]
        public int Tipo { get; set; }
    }
}
