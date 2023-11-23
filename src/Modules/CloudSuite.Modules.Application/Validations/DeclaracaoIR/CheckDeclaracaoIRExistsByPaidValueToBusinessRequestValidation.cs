using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.DeclaracaoIR
{
    public class CheckDeclaracaoIRExistsByPaidValueToBusinessRequestValidation : AbstractValidator<CheckDeclaracaoIRExistsByPaidValueToBusinessRequest>
    {
        public CheckDeclaracaoIRExistsByPaidValueToBusinessRequestValidation()
        {
            RuleFor(a => a.PaidValueToBusiness)
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor pago ao negócio deve ser maior ou igual a 0.");
        }
    }
}
