using CloudSuite.Modules.Application.Handlers.IdeCancelamento.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.IdeCancelamento
{
    public class CheckIdeCancelamentoExistsByTimeDateRequestValidation : AbstractValidator<CheckIdeCancelamentoExistsByTimeDateRequest>
    {
        public CheckIdeCancelamentoExistsByTimeDateRequestValidation()
        {
            RuleFor(a => a.TimeDate)
            .NotNull()
            .WithMessage("A data e hora não podem ser nulas.")
            .LessThanOrEqualTo(DateTimeOffset.Now)
            .WithMessage("A data e hora não podem ser no futuro.");
        }
    }
}
