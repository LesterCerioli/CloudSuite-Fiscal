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
    public class NoteViewModel
    {
        [Key]
        public Guid Id { get; private set; }

        [DisplayName("Tomador de Serviço")]
        public TomadorServico TomadorServico { get; private set; }

        [DisplayName("Endereço")]
        public Address Address { get; private set; }

        [DisplayName("Pais")]
        public Country Country { get; private set; }

        [DisplayName("Distrito")]
        public District District { get; private set; }

        [DisplayName("Prestador de Serviço")]
        public Prestador Prestador { get; private set; }

        [DisplayName("Cnpj")]
        public Cnpj Cnpj { get; private set; }

        [DisplayName("Numero da Nota")]
        public string? NoteNumber { get; private set; }

        [DisplayName("Data de Emissão da Nota")]
        public DateTime? EmissionDate { get; private set; }

        [DisplayName("Valor")]
        public decimal? Value { get; private set; }
    }
}
