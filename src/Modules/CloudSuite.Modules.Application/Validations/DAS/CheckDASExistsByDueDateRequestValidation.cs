using CloudSuite.Modules.Application.Handlers.DAS.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.DAS
{
    public class CheckDASExistsByDueDateRequestValidation : AbstractValidator<CheckDASExistsByDueDateRequest>
    {
        public CheckDASExistsByDueDateRequestValidation()
        {
            RuleFor(a => a.DueDate)
                .NotNull()
                .WithMessage("A data de vencimento não pode ser nula.");
        }
    }
}
