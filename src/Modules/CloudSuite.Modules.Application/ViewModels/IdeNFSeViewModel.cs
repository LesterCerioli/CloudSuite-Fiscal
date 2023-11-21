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
        public Guid Id { get; private set; }

        [DisplayName("Numero do NFSe")]
        public string? Number { get; private set; }

        [DisplayName("Chave do NFSe")]
        public string? Key { get; private set; }

        [DisplayName("Data de Emissão")]
        public DateTime? EmissionDate { get; private set; }

        [DisplayName("Nota do NFSe")]
        public Note Note { get; private set; }
    }
}
