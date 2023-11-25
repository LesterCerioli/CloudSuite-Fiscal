using CloudSuite.Modules.Application.Handlers.Darf.Responses;
using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarfEntity = CloudSuite.Modules.Domain.Models.Darf;
using PrestadorEntity = CloudSuite.Modules.Domain.Models.Prestador;

namespace CloudSuite.Modules.Application.Handlers.Darf
{
    public class CreateDarfCommand : IRequest<CreateDarfResponse>
    {
        public Guid Id { get; private set; }

        public string ReferenceMonth { get; set; }

        public DateTime DueDate { get; set; }

        public string ReferenceYear { get; set; }

        public decimal? DarfPaymentValue { get; set; }

        public string? RecuboDeclaroNumero { get; set; }

        public string DocumentNumber { get; set; }

        public string? BarCode { get; set; }

        public DateTime ValidationDate { get; set; }

        public DateTime? PeriodoApuracao { get; set; }

        public Cnpj Cnpj { get; set; }

        public string? ReceitaCode { get; set; }

        public string? MainValue { get; set; }

        public decimal? AmountFine { get; set; }
        
        public bool? IsInstallment { get; set; }
        
        public decimal? Interest { get; set; }

        public decimal? TotalValue { get; set; }

        public PrestadorEntity Prestador { get; set; }

        public CreateDarfCommand()
        {
            Id = Guid.NewGuid();
        }

        public DarfEntity GetEntity()
        {
            return new DarfEntity(
                this.ReferenceMonth,
                this.DueDate,
                this.ReferenceYear,
                this.DarfPaymentValue,
                this.RecuboDeclaroNumero,
                this.DocumentNumber,
                this.BarCode,
                this.ValidationDate,
                this.PeriodoApuracao,
                this.Cnpj,
                this.ReceitaCode,
                this.MainValue,
                this.AmountFine,
                this.Interest,
                this.TotalValue
                );
        }
    }
}
