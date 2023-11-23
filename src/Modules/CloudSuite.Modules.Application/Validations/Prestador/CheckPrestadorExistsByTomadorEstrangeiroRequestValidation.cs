using CloudSuite.Modules.Application.Handlers.Prestador.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.Prestador
{
    public class CheckPrestadorExistsByTomadorEstrangeiroRequestValidation : AbstractValidator<CheckPrestadorExistsByTomadorEstrangeiroRequest>
    {
        public CheckPrestadorExistsByTomadorEstrangeiroRequestValidation()
        {
            RuleFor(a => a.DocTomadorEstrangeiro)
                .MaximumLength(100)
                .WithMessage("O documento do tomador estrangeiro não pode ter mais de 100 caracteres.");
        }
    }
}
