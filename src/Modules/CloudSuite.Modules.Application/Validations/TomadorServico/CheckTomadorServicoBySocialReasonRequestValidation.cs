using CloudSuite.Modules.Application.Handlers.TomadorServico.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.TomadorServico
{
    public class CheckTomadorServicoBySocialReasonRequestValidation : AbstractValidator<CheckTomadorServicoBySocialReasonRequest>
    {
        public CheckTomadorServicoBySocialReasonRequestValidation ()
        {
            RuleFor(a => a.SocialReason)
                .NotNull()
                .WithMessage("A razão social não pode ser nula.")
                .MaximumLength(100)
                .WithMessage("A razão social não pode ter mais de 100 caracteres.");
        }
    }
}
