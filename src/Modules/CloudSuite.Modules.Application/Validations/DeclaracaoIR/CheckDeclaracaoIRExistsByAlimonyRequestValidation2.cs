using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.DeclaracaoIR
{
    public class CheckDeclaracaoIRExistsByAlimonyRequestValidation : AbstractValidator<CheckDeclaracaoIRExistsByAlimonyRequest>
    {
        public CheckDeclaracaoIRExistsByAlimonyRequestValidation()
        {
            RuleFor(a => a.Alimony)
                .GreaterThanOrEqualTo(0)
                .WithMessage("A pensão alimentícia deve ser maior ou igual a 0.");
        }
    }
}
