using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.DeclaracaoIR
{
    public class CheckDeclaracaoIRExistsByTotalIncomeRequestValidation : AbstractValidator<CheckDeclaracaoIRExistsByTotalIncomeRequest>
    {
        public CheckDeclaracaoIRExistsByTotalIncomeRequestValidation()
        {
            RuleFor(a => a.TotalIncome)
                .NotNull()
                .WithMessage("O esse campo não pode ser nulo.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("A renda total deve ser maior ou igual a 0.");
        }
    }
}
