using CloudSuite.Modules.Application.Handlers.City;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.City
{
    public class CreateCityCommandValidation : AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidation() 
        {
            RuleFor(a => a.CityName)
            .NotNull()
            .WithMessage("O nome da cidade não pode ser nulo.")
            .MaximumLength(100)
            .WithMessage("O nome da cidade não pode ter mais de 100 caracteres.")
            .MinimumLength(2)
            .WithMessage("O nome da cidade deve ter pelo menos 2 caracteres.")
            .Matches(@"^[a-zA-Z\s]*$")
            .WithMessage("O nome da cidade só pode conter letras e espaços.");

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
