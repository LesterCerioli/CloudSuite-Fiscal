using CloudSuite.Modules.Common.ValueObjects;
using NetDevPack.Domain;

namespace CloudSuite.Modules.Domain.Models
{
    public class DeclaracaoIR : Entity, IAggregateRoot
    {
        public DeclaracaoIR(string? declaracoaNumero, Cnpj cnpj, Cpf cpf, string? companyName, string? businessHeader, decimal? totalIncome, decimal? socialSecurity, decimal? complementContribution, decimal? alimony, string? taxWithheld, decimal? paidValueToBusiness, decimal? profitsDividends)
        {
            DeclaracoaNumero = declaracoaNumero;
            Cnpj = cnpj;
            Cpf = cpf;
            CompanyName = companyName;
            BusinessHeader = businessHeader;
            TotalIncome = totalIncome;
            SocialSecurity = socialSecurity;
            ComplementContribution = complementContribution;
            Alimony = alimony;
            TaxWithheld = taxWithheld;
            PaidValueToBusiness = paidValueToBusiness;
            ProfitsDividends = profitsDividends;
        }

        public string? DeclaracoaNumero { get; private set; }

        public Cnpj Cnpj { get; private set; }

        public Cpf Cpf { get; private set; }

        public string? CompanyName { get; private set; }

        public string? BusinessHeader { get; private set; }

        // Total de Rendimentos
        public decimal? TotalIncome { get; private set; }

        // Contribuição previdenciária
        public decimal? SocialSecurity { get; private set; }

        public decimal? ComplementContribution { get; private set; }

        public decimal? Alimony { get; private set; }

        // Imposto retido na fonte
        public string? TaxWithheld { get; private set; }

        public decimal? PaidValueToBusiness { get; private set; }

        public decimal? ProfitsDividends { get; private set; }
        
    }
}