using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
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

        public string? Number { get; private set; }

        public string? Key { get; private set; }

        public DateTime? EmissionDate { get; private set; }

        public Note Note { get; private set; }
    }
}
