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
        public Guid Id { get; set; }

        [DisplayName("Numero da Declaração do Imposto de Renda")]
        [Required(ErrorMessage = "The field is required.")]
        public string DeclaracaoNumero { get; set; }

        [DisplayName("Nome da Companhia da Declaração do Imposto de Renda")]
        [Required(ErrorMessage = "The field is required.")]
        public string CompanyName { get; set; }

        [DisplayName("Header do negocio na Declaração do Imposto de Renda")]
        [Required(ErrorMessage = "The field is required.")]
        public string BusinessHeader { get; set; }

        [DisplayName("Renda Total na Declaração do Imposto de Renda")]
        [Required(ErrorMessage = "The field is required.")]
        public decimal TotalIncome { get; set; }

        [DisplayName("Contribuição para a Segurança Social na Declaração do Imposto de Renda")]
        [Required(ErrorMessage = "The field is required.")]
        public decimal SocialSecurity { get; set; }

        [DisplayName("Contribuição para a Segurança Social na Declaração do Imposto de Renda")]
        [Required(ErrorMessage = "The field is required.")]
        public decimal ComplementContribution { get; set; }

        [DisplayName("pensão alimentícia na Declaração do Imposto de Renda")]
        [Required(ErrorMessage = "The field is required.")]
        public decimal Alimony { get; set; }

        [DisplayName("Imposto Retido na Fonte na Declaração do Imposto de Renda")]
        [Required(ErrorMessage = "The field is required.")]
        public string TaxWithheld { get; set; }

        [DisplayName("valor pago ao negócio na Declaração do Imposto de Renda")]
        [Required(ErrorMessage = "The field is required.")]
        public decimal PaidValueToBusiness { get; set; }

        [DisplayName("valor pago ao negócio na Declaração do Imposto de Renda")]
        [Required(ErrorMessage = "The field is required.")]
        public decimal ProfitsDividends { get; set; }
    }
}
