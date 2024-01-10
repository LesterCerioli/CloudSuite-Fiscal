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

        [DisplayName("Numero da Declaração")]
        [Required(ErrorMessage = "Campo Numero da Declaração é obrigatorio.")]
        public string DeclaracaoNumero { get; set; }

        [DisplayName("Nome da Empresa")]
        [Required(ErrorMessage = "Campo Nome da Empresa é obrigatorio.")]
        public string CompanyName { get; set; }

        [DisplayName("Cnpj")]
        [Required(ErrorMessage = "Campo Cnpj é obrigatorio.")]
        public string Cnpj { get; set; }

        [DisplayName("Cpf")]
        [Required(ErrorMessage = "Campo Cpf é obrigatorio.")]
        public string Cpf { get; set; }

        [DisplayName("Header do negocio")]
        [Required(ErrorMessage = "Campo Header do negocio é obrigatorio.")]
        public string BusinessHeader { get; set; }

        [DisplayName("Total de Rendimentos")]
        [Required(ErrorMessage = "Campo Renda Total é obrigatorio.")]
        public decimal TotalIncome { get; set; }

        [DisplayName("Contribuição Previdenciaria")]
        [Required(ErrorMessage = "Campo Contribuição Previdenciaria é obrigatorio.")]
        public decimal SocialSecurity { get; set; }

        [DisplayName("Contribuição Complementar")]
        [Required(ErrorMessage = "Campo Contribuição complementar é obrigatorio.")]
        public decimal ComplementContribution { get; set; }

        [DisplayName("pensão alimentícia")]
        [Required(ErrorMessage = "Campo Pensão Alimentícia é obrigatorio.")]
        public decimal Alimony { get; set; }

        [DisplayName("Imposto Retido na Fonte")]
        [Required(ErrorMessage = "Campo Imposto Retido na Fonte é obrigatorio.")]
        public string TaxWithheld { get; set; }

        [DisplayName("valor pago ao negócio")]
        [Required(ErrorMessage = "Campo Valor pago ao negócio é obrigatorio.")]
        public decimal PaidValueToBusiness { get; set; }

        [DisplayName("Dividendos")]
        [Required(ErrorMessage = "Campo Dividendos é obrigatorio.")]
        public decimal ProfitsDividends { get; set; }

    }
}
