using CloudSuite.Modules.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.ViewModels
{
    public class DeclaracaoIRViewModel
    {
        [Key]
        public Guid Id { get; private set; }

        public string? DeclaracaoNumero { get; private set; }

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
    }
}
