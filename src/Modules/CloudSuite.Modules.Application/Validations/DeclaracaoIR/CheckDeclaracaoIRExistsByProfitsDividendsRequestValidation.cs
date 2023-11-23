using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.DeclaracaoIR
{
    public class CheckDeclaracaoIRExistsByProfitsDividendsRequestValidation : AbstractValidator<CheckDeclaracaoIRExistsByProfitsDividendsRequest>
    {
        public CheckDeclaracaoIRExistsByProfitsDividendsRequestValidation()
        {
            RuleFor(a => a.ProfitsDividends)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Os lucros e dividendos devem ser maior ou igual a 0.");
        }
    }
}
