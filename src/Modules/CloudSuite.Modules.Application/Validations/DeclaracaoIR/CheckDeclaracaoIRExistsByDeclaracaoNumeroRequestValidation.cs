using CloudSuite.Modules.Application.Handlers.DeclaracaoIR.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.DeclaracaoIR
{
    public class CheckDeclaracaoIRExistsByDeclaracaoNumeroRequestValidation : AbstractValidator<CheckDeclaracaoIRExistsByDeclaracaoNumeroRequest>
    {
        public CheckDeclaracaoIRExistsByDeclaracaoNumeroRequestValidation()
        {
            RuleFor(a => a.DeclaracoaNumero)
                .NotNull()
                .WithMessage("O Numero da declaração não pode ser nulo.")
                .MaximumLength(100)
                .WithMessage("O número da declaração não pode ter mais de 100 caracteres.");
        }
    }
}
