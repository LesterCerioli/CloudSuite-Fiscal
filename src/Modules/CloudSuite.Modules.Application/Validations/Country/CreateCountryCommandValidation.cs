using CloudSuite.Modules.Application.Handlers.Country;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.Country
{
    public class CreateCountryCommandValidation : AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryCommandValidation() 
        {
            RuleFor(a => a.CountryName)
            .NotNull()
            .WithMessage("O nome do país não pode ser nulo.")
            .MaximumLength(100)
            .WithMessage("O nome do país não pode ter mais de 100 caracteres.")
            .MinimumLength(2)
            .WithMessage("O nome do país deve ter pelo menos 2 caracteres.")
            .Matches(@"^[a-zA-Z\s]*$")
            .WithMessage("O nome do país só pode conter letras e espaços.");

            RuleFor(a => a.Code3)
            .NotNull()
            .WithMessage("O código de 3 letras não pode ser nulo.")
            .Matches(@"^[A-Z]*$")
            .WithMessage("O código de 3 letras só pode conter letras maiúsculas.");

            RuleFor(a => a.IsBillingEnabled)
            .NotNull()
            .WithMessage("A habilitação para faturamento não pode ser nula.");

            RuleFor(a => a.IsShippingEnabled)
            .NotNull()
            .WithMessage("A habilitação para envio não pode ser nula.");

            RuleFor(a => a.IsCityEnabled)
            .NotNull()
            .WithMessage("A habilitação para cidade não pode ser nula.");

            RuleFor(a => a.IsZipCodeEnabled)
            .NotNull()
            .WithMessage("A habilitação para código postal não pode ser nula.");

            RuleFor(a => a.IsDistrictEnabled)
            .NotNull()
            .WithMessage("A habilitação para distrito não pode ser nula.");
        }
    }
}
