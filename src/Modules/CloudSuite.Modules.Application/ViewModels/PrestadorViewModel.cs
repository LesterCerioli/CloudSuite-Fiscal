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
        public Guid Id { get; private set; }

        [DisplayName("Cnpj do Prestador de serviço")]
        public Cnpj Cnpj { get; private set; }

        [DisplayName("Inscrição Municipal do Prestador de Serviço")]
        public string? InscricaoMunicipal { get; private set; }

        [DisplayName("Inscrição Estadual do Prestador de Serviço")]
        public string? InscricaoEstadual { get; private set; }

        [DisplayName("Documento Estrangeiro do Prestador de Serviço")]
        public string? DocTomadorEstrangeiro { get; private set; }

        [DisplayName("Razão Social do Prestador de Serviço")]
        public string? SocialReason { get; private set; }

        [DisplayName("Nome Fantasia do Prestador de Serviço")]
        public string NomeFantasia { get; set; }

        [DisplayName("Endereço do Prestador de Serviço")]
        public Address Address { get; private set; }

        [DisplayName("Tipo")]
        public int Tipo { get; set; }

    }
}
