using CloudSuite.Modules.Application.Handlers.District;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.District
{
    public class CreateDistrictCommandValidation : AbstractValidator<CreateDistrictCommand>
    {
        public CreateDistrictCommandValidation()
        {
            RuleFor(a => a.Name)
            .NotNull()
            .WithMessage("O nome não pode ser nulo.")
            .MaximumLength(100)
            .WithMessage("O nome não pode ter mais de 100 caracteres.")
            .MinimumLength(2)
            .WithMessage("O nome deve ter pelo menos 2 caracteres.")
            .Matches(@"^[a-zA-Z\s]*$")
            .WithMessage("O nome só pode conter letras e espaços.");

            RuleFor(a => a.Type)
            .NotNull()
            .WithMessage("O tipo não pode ser nulo.")
            .MaximumLength(100)
            .WithMessage("O tipo não pode ter mais de 100 caracteres.")
            .MinimumLength(2)
            .WithMessage("O tipo deve ter pelo menos 2 caracteres.")
            .Matches(@"^[a-zA-Z\s]*$")
            .WithMessage("O tipo só pode conter letras e espaços.");

            RuleFor(a => a.Location)
            .NotNull()
            .WithMessage("A localização não pode ser nula.")
            .MaximumLength(100)
            .WithMessage("A localização não pode ter mais de 100 caracteres.")
            .MinimumLength(2)
            .WithMessage("A localização deve ter pelo menos 2 caracteres.")
            .Matches(@"^[a-zA-Z\s]*$")
            .WithMessage("A localização só pode conter letras e espaços.");

            RuleFor(a => a.State.StateName)
            .NotNull()
            .WithMessage("O nome do estado não pode ser nulo.")
            .MaximumLength(100)
            .WithMessage("O nome do estado não pode ter mais de 100 caracteres.")
            .MinimumLength(2)
            .WithMessage("O nome do estado deve ter pelo menos 2 caracteres.")
            .Matches(@"^[a-zA-Z\s]*$")
            .WithMessage("O nome do estado só pode conter letras e espaços.");

            RuleFor(a => a.State.UF)
            .NotNull()
            .WithMessage("A UF não pode ser nula.")
            .Length(2)
            .WithMessage("A UF deve ter exatamente 2 caracteres.")
            .Matches(@"^[A-Z]*$")
            .WithMessage("A UF só pode conter letras maiúsculas.");
        }
    }
}
