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
    public class PrestadorViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Inscrição Municipal do Prestador de Serviço")]
        [Required(ErrorMessage = "The field is required.")]
        public string InscricaoMunicipal { get; set; }

        [DisplayName("Inscrição Estadual do Prestador de Serviço")]
        [Required(ErrorMessage = "The field is required.")]
        public string InscricaoEstadual { get; set; }

        [DisplayName("Documento Estrangeiro do Prestador de Serviço")]
        [Required(ErrorMessage = "The field is required.")]
        public string DocTomadorEstrangeiro { get; set; }

        [DisplayName("Razão Social do Prestador de Serviço")]
        [Required(ErrorMessage = "The field is required.")]
        public string SocialReason { get; set; }

        [DisplayName("Nome Fantasia do Prestador de Serviço")]
        [Required(ErrorMessage = "The field is required.")]
        public string NomeFantasia { get; set; }

        [DisplayName("Tipo")]
        [Required(ErrorMessage = "The field is required.")]
        public int Tipo { get; set; }

    }
}
