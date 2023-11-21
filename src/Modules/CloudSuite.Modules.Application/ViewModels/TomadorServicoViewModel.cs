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

        [DisplayName("Cnpj do Tomador de serviço")]
        public Cnpj Cnpj { get; private set; }

        [DisplayName("Inscricao Municipal do Tomador de Serviço")]
        public string? InscricaoMunicipal { get; private set; }

        [DisplayName("Inscricao Estadual do Tomador de Serviço")]
        public string? InscricaoEstadual { get; private set; }

        [DisplayName("Documento Estrangeiro do Tomador de Serviço")]
        public string? DocTomadorEstrangeiro { get; private set; }

        [DisplayName("Razão Social do Tomador de Serviço")]
        public string? SocialReason { get; private set; }

        [DisplayName("Nome Fantasia do Tomador de Serviço")]
        public string NomeFantasia { get; set; }

        [DisplayName("Endereço do Tomador de Serviço")]
        public Address Address { get; private set; }

        [DisplayName("Tipo do Tomador de Serviço")]
        public int Tipo { get; set; }
    }
}
