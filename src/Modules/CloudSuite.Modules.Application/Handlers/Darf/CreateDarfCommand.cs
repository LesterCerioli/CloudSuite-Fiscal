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
        public Guid Id { get; set; }

        public string ReferenceMonth { get; private set; }

        public DateTime DueDate { get; private set; }

        public string ReferenceYear { get; private set; }

        public decimal? DarfPaymentValue { get; private set; }

        public string? RecuboDeclaroNumero { get; private set; }

        public string DocumentNumber { get; private set; }

        public string? BarCode { get; private set; }

        public DateTime ValidationDate { get; private set; }

        public DateTime? PeriodoApuracao { get; private set; }

        public Cnpj Cnpj { get; private set; }

        public string? ReceitaCode { get; private set; }

        public string? MainValue { get; private set; }

        public decimal? AmountFine { get; private set; }
        
        public bool? IsInstallment { get; private set; }
        
        public decimal? Interest { get; private set; }

        public decimal? TotalValue { get; private set; }

        public PrestadorEntity Prestador { get; private set; }

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
