using CloudSuite.Modules.Application.Handlers.Darf.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.Darf
{
    public class CheckDarfExistsByDueDateRequestValidation : AbstractValidator<CheckDarfExistsByDueDateRequest>
    {
        public CheckDarfExistsByDueDateRequestValidation()
        {
            RuleFor(a => a.DueDate)
                .NotNull()
                .WithMessage("A data de vencimento não pode ser nula.");
        }
    }
}
