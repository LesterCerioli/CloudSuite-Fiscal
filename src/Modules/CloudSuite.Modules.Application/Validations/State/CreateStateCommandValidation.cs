using CloudSuite.Modules.Application.Handlers.State;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.State
{
    public class CreateStateCommandValidation : AbstractValidator<CreateStateCommand>
    {
        public CreateStateCommandValidation() 
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

            RuleFor(a => a.UF)
            .NotNull()
            .WithMessage("A UF não pode ser nula.")
            .Length(2)
            .WithMessage("A UF deve ter exatamente 2 caracteres.")
            .Matches(@"^[A-Z]*$")
            .WithMessage("A UF só pode conter letras maiúsculas.");
            
            RuleFor(a => a.Country.CountryName)
            .NotNull()
            .WithMessage("O nome do país não pode ser nulo.")
            .MaximumLength(100)
            .WithMessage("O nome do país não pode ter mais de 100 caracteres.")
            .MinimumLength(2)
            .WithMessage("O nome do país deve ter pelo menos 2 caracteres.")
            .Matches(@"^[a-zA-Z\s]*$")
            .WithMessage("O nome do país só pode conter letras e espaços.");

            RuleFor(a => a.Country.Code3)
            .NotNull()
            .WithMessage("O código de 3 letras não pode ser nulo.")
            .Matches(@"^[A-Z]*$")
            .WithMessage("O código de 3 letras só pode conter letras maiúsculas.");

            RuleFor(a => a.Country.IsBillingEnabled)
            .NotNull()
            .WithMessage("A habilitação para faturamento não pode ser nula.");

            RuleFor(a => a.Country.IsShippingEnabled)
            .NotNull()
            .WithMessage("A habilitação para envio não pode ser nula.");

            RuleFor(a => a.Country.IsCityEnabled)
            .NotNull()
            .WithMessage("A habilitação para cidade não pode ser nula.");

            RuleFor(a => a.Country.IsZipCodeEnabled)
            .NotNull()
            .WithMessage("A habilitação para código postal não pode ser nula.");

            RuleFor(a => a.Country.IsDistrictEnabled)
            .NotNull()
            .WithMessage("A habilitação para distrito não pode ser nula.");
        }
    }
}
