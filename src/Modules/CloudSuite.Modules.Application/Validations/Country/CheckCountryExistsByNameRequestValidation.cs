using CloudSuite.Modules.Application.Handlers.Country.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.Country
{
    public class CheckCountryExistsByNameRequestValidation : AbstractValidator<CheckCountryExistsByNameRequest>
    {
        public CheckCountryExistsByNameRequestValidation() 
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
        }
    }
}
