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
        public string? DeclaracoaNumero { get; private set; }

        public Cnpj Cnpj { get; private set; }

        public Cpf Cpf { get; private set; }

        public string? CompanyName { get; private set; }

        public string? BusinessHeader { get; private set; }

        public decimal? TotalIncome { get; private set; }

        public decimal? SocialSecurity { get; private set; }

        public decimal? ComplementContribution { get; private set; }

        public decimal? Alimony { get; private set; }

        public string? TaxWithheld { get; private set; }

        public decimal? PaidValueToBusiness { get; private set; }

        public decimal? ProfitsDividends { get; private set; }


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
