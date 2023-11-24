using CloudSuite.Modules.Application.Handlers.IdeCancelamento.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.IdeCancelamento
{
    public class CheckIdeCancelamentoExistsByCancelReasonRequestValidation : AbstractValidator<CheckIdeCancelamentoExistsByCancelReasonRequest>
    {
        public CheckIdeCancelamentoExistsByCancelReasonRequestValidation()
        {
            RuleFor(a => a.CancelReason)
            .MaximumLength(200)
            .WithMessage("O motivo do cancelamento não pode ter mais de 200 caracteres.");
        }
    }
}
