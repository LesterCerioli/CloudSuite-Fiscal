using CloudSuite.Modules.Application.Handlers.City.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.City
{
    public class CheckCityExistsByNameRequestValidation : AbstractValidator<CheckCityExistsByNameRequest>
    {
        public CheckCityExistsByNameRequestValidation() 
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
        }
    }
}
