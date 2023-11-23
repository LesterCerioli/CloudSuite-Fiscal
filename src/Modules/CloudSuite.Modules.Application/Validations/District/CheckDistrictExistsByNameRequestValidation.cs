using CloudSuite.Modules.Application.Handlers.District.Requests;
using CloudSuite.Modules.Application.Handlers.District.Responses;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.District
{
    public class CheckDistrictExistsByNameRequestValidation : AbstractValidator<CheckDistrictExistsByNameRequest>
    {
        public CheckDistrictExistsByNameRequestValidation() 
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
        }
    }
}

