using CloudSuite.Modules.Application.Handlers.Address;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.Address
{
    public class CreateAddressCommandValidation : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidation()
        {
            RuleFor(a => a.ContactName)
           .NotEmpty()
           .MaximumLength(40)
           .WithMessage("O nome está incorreto.");

            RuleFor(a => a.AddressLine1)
            .NotEmpty()
            .WithMessage("O endereço não pode estar vazio.")
            .MaximumLength(100)
            .WithMessage("O endereço não pode ter mais de 100 caracteres.")
            .MinimumLength(2)
            .WithMessage("O endereço deve ter pelo menos 2 caracteres.")
            .Matches(@"^[a-zA-Z0-9\s,]*$")
            .WithMessage("O endereço só pode conter letras, números, espaços e vírgulas.");

            RuleFor(a => a.City)
            .NotNull()
            .WithMessage("A cidade não pode ser nula.")
            .Must(city => city.CityName.Length <= 100)
            .WithMessage("O nome da cidade não pode ter mais de 100 caracteres.")
            .Must(city => city.CityName.Length >= 2)
            .WithMessage("O nome da cidade deve ter pelo menos 2 caracteres.")
            .Must(city => Regex.IsMatch(city.CityName, @"^[a-zA-Z\s]*$"))
            .WithMessage("O nome da cidade só pode conter letras e espaços.");

            RuleFor(a => a.District.Name)
            .NotNull()
            .WithMessage("O nome não pode ser nulo.")
            .MaximumLength(100)
            .WithMessage("O nome não pode ter mais de 100 caracteres.")
            .MinimumLength(2)
            .WithMessage("O nome deve ter pelo menos 2 caracteres.")
            .Matches(@"^[a-zA-Z\s]*$")
            .WithMessage("O nome só pode conter letras e espaços.");

            RuleFor(a => a.District.Type)
            .NotNull()
            .WithMessage("O tipo não pode ser nulo.")
            .MaximumLength(50)
            .WithMessage("O tipo não pode ter mais de 50 caracteres.")
            .Matches(@"^[a-zA-Z\s]*$")
            .WithMessage("O tipo só pode conter letras e espaços.");

            RuleFor(a => a.District.Location)
            .NotNull()
            .WithMessage("A localização não pode ser nula.")
            .MaximumLength(100)
            .WithMessage("A localização não pode ter mais de 100 caracteres.")
            .MinimumLength(10)
            .WithMessage("A localização deve ter pelo menos 10 caracteres.")
            .Matches(@"^[a-zA-Z0-9\s,]*$")
            .WithMessage("A localização só pode conter letras, números, espaços e vírgulas.");

        }
    }
}
