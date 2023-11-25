using CloudSuite.Modules.Application.Handlers.DAS.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.DAS
{
    public class CheckDASExistsByReferenceYearRequestValidation : AbstractValidator<CheckDASExistsByReferenceYearRequest>
    {
        public CheckDASExistsByReferenceYearRequestValidation()
        {
            RuleFor(a => a.ReferenceYear)
                .NotNull()
                .WithMessage("O número do documento não pode ser nulo.")
                .MaximumLength(4)
                .WithMessage("O ano de referência não pode ter mais de 4 caracteres.")
                .Matches(@"^\d{4}$")
                .WithMessage("O ano de referência deve ser um número de 4 dígitos.");
        }
    }
}
