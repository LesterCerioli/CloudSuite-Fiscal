using CloudSuite.Modules.Application.Handlers.Darf.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.Darf
{
    public class CheckDarfExistsByReferenceMonthRequestValidation : AbstractValidator<CheckDarfExistsByReferenceMonthRequest>
    {
        public CheckDarfExistsByReferenceMonthRequestValidation()
        {
            RuleFor(a => a.ReferenceMonth)
                .NotNull()
                .WithMessage("O mês de referência não pode ser nulo.")
                .MaximumLength(2)
                .WithMessage("O mês de referência não pode ter mais de 2 caracteres.")
                .Matches(@"^(0[1-9]|1[0-2])$")
                .WithMessage("O mês de referência deve ser um valor entre 01 e 12.");
        }
    }
}
