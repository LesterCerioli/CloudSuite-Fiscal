using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Responses;
using CloudSuite.Modules.Common.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeclaracaoIREntity = CloudSuite.Modules.Domain.Models.DeclaracaoIR;

namespace CloudSuite.Modules.Application.Handlers.DeclaracaoIR
{
    public class CreateDeclaracaoIRCommand : IRequest<CreateDeclaracaoIRResponse>
    {
        public Guid Id { get; private set; }

        public string? DeclaracoaNumero { get; set; }

        public Cnpj Cnpj { get; set; }

        public Cpf Cpf { get; set; }

        public string? CompanyName { get; set; }

        public string? BusinessHeader { get; set; }

        public decimal TotalIncome { get; set; }

        public decimal? SocialSecurity { get; set; }

        public decimal? ComplementContribution { get; set; }

        public decimal Alimony { get; set; }

        public string? TaxWithheld { get; set; }

        public decimal PaidValueToBusiness { get; set; }

        public decimal ProfitsDividends { get; set; }

        public CreateDeclaracaoIRCommand()
        {
            Id = Guid.NewGuid();
        }

        public DeclaracaoIREntity GetEntity()
        {
            return new DeclaracaoIREntity(
                this.DeclaracoaNumero,
                this.Cnpj,
                this.Cpf,
                this.CompanyName,
                this.BusinessHeader,
                this.TotalIncome,
                this.SocialSecurity,
                this.ComplementContribution,
                this.Alimony,
                this.TaxWithheld,
                this.PaidValueToBusiness,
                this.ProfitsDividends
                );
        }

    }
}
