using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
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

        public Cnpj Cnpj { get; private set; }

        public string? InscricaoMunicipal { get; private set; }

        public string? InscricaoEstadual { get; private set; }

        public string? DocTomadorEstrangeiro { get; private set; }

        public string? SocialReason { get; private set; }

        public string NomeFantasia { get; set; }

        public Address Address { get; private set; }

        public int Tipo { get; set; }

    }
}
