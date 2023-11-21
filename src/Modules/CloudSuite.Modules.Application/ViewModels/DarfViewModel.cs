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
    public class DarfViewModel
    {
        [Key]
        public Guid Id { get; private set; }

        public string? ReferenceMonth { get; private set; }

        public DateTime? DueDate { get; private set; }

        public string? ReferenceYear { get; private set; }

        public decimal? DarfPaymentValue { get; private set; }

        public string? RecuboDeclaroNumero { get; private set; }

        public string? DocumentNumber { get; private set; }

        public string? BarCode { get; private set; }

        public DateTime? ValidationDate { get; private set; }

        public DateTime? PeriodoApuracao { get; private set; }

        public Cnpj Cnpj { get; private set; }

        public string? ReceitaCode { get; private set; }

        public string? MainValue { get; private set; }

        public decimal? AmountFine { get; private set; }
        
        public bool? IsInstallment { get; private set; }

        public decimal? Interest { get; private set; }

        public decimal? TotalValue { get; private set; }

        public Prestador Prestador { get; private set; }
    }
}
