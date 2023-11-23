using CloudSuite.Modules.Application.Handlers.State.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.State
{
    public class CheckStateExistsByNameRequestValidation : AbstractValidator<CheckStateExistsByNameRequest>
    {
        public CheckStateExistsByNameRequestValidation() 
        {
            RuleFor(a => a.StateName)
            .NotNull()
            .WithMessage("O nome do estado não pode ser nulo.")
            .MaximumLength(100)
            .WithMessage("O nome do estado não pode ter mais de 100 caracteres.")
            .MinimumLength(2)
            .WithMessage("O nome do estado deve ter pelo menos 2 caracteres.")
            .Matches(@"^[a-zA-Z\s]*$")
            .WithMessage("O nome do estado só pode conter letras e espaços.");
        }
    }
}
