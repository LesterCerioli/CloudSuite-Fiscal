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
        public Guid Id { get; set; }

        [DisplayName("Tomador de Serviço")]
        [Required(ErrorMessage = "Campo Tomador Servico é obrigatorio.")]
        public string TomadorServico { get; set; }

        [DisplayName("Endereço")]
        [Required(ErrorMessage = "Campo Endereço é obrigatorio.")]
        public string Address { get; set; }

        [DisplayName("País")]
        [Required(ErrorMessage = "Campo País é obrigatorio.")]
        public string Country { get; set; }

        [DisplayName("Distrito")]
        [Required(ErrorMessage = "Campo Distrito é obrigatorio.")]
        public string District { get; set; }

        [DisplayName("Prestador de Serviço")]
        [Required(ErrorMessage = "Campo Prestador de Serviço é obrigatorio.")]
        public string Prestador { get; set; }

        [DisplayName("Cnpj")]
        [Required(ErrorMessage = "Campo Cnpj é obrigatorio.")]
        public string Cnpj { get; set; }

        [DisplayName("Numero da Nota")]
        [Required(ErrorMessage = "Campo Numero da Nota é obrigatorio.")]
        public string NoteNumber { get; set; }

        [DisplayName("Data de Emissão da Nota")]
        [Required(ErrorMessage = "Campo Data de Emissão da Nota é obrigatorio.")]
        public DateTime EmissionDate { get; set; }

        [DisplayName("Valor")]
        [Required(ErrorMessage = "Campo Valor é obrigatorio.")]
        public decimal Value { get; set; }
    }
}
