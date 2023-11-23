using CloudSuite.Modules.Application.Handlers.State.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.State
{
    public class CheckStateExistsByUfRequestValidation : AbstractValidator<CheckStateExistsByUfRequest>
    {
        public CheckStateExistsByUfRequestValidation() 
        {
            RuleFor(a => a.UF)
                .NotNull()
                .WithMessage("A UF não pode ser nula.")
                .Length(2)
                .WithMessage("A UF deve ter exatamente 2 caracteres.")
                .Matches(@"^[A-Z]*$")
                .WithMessage("A UF só pode conter letras maiúsculas.");
        }
    }
}
