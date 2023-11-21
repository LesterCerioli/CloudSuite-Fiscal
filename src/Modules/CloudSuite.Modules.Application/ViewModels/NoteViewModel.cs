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
    public class NoteViewModel
    {
        [Key]
        public Guid Id { get; private set; }

        public TomadorServico TomadorServico { get; private set; }

        public Address Address { get; private set; }

        public Country Country { get; private set; }

        public District District { get; private set; }

        public Prestador Prestador { get; private set; }

        public Cnpj Cnpj { get; private set; }

        public string? NoteNumber { get; private set; }

        public DateTime? EmissionDate { get; private set; }

        public decimal? Value { get; private set; }
    }
}
