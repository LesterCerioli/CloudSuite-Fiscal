using CloudSuite.Modules.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("Numero da Declaração do Imposto de Renda")]
        public string? DeclaracaoNumero { get; private set; }

        [DisplayName("Cnpj da Declaração do Imposto de Renda")]
        public Cnpj Cnpj { get; private set; }

        [DisplayName("Cpf da Declaração do Imposto de Renda")]
        public Cpf Cpf { get; private set; }

        [DisplayName("Nome da Companhia da Declaração do Imposto de Renda")]
        public string? CompanyName { get; private set; }

        [DisplayName("Header do negocio na Declaração do Imposto de Renda")]
        public string? BusinessHeader { get; private set; }

        [DisplayName("Renda Total na Declaração do Imposto de Renda")]
        public decimal? TotalIncome { get; private set; }

        [DisplayName("Contribuição para a Segurança Social na Declaração do Imposto de Renda")]
        public decimal? SocialSecurity { get; private set; }

        [DisplayName("Contribuição para a Segurança Social na Declaração do Imposto de Renda")]
        public decimal? ComplementContribution { get; private set; }

        [DisplayName("pensão alimentícia na Declaração do Imposto de Renda")]
        public decimal? Alimony { get; private set; }

        [DisplayName("Imposto Retido na Fonte na Declaração do Imposto de Renda")]
        public string? TaxWithheld { get; private set; }

        [DisplayName("valor pago ao negócio na Declaração do Imposto de Renda")]
        public decimal? PaidValueToBusiness { get; private set; }

        [DisplayName("valor pago ao negócio na Declaração do Imposto de Renda")]
        public decimal? ProfitsDividends { get; private set; }
    }
}
