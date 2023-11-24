using CloudSuite.Modules.Application.Handlers.Prestador.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.Prestador
{
    public class CheckPrestadorExistsByInscricaoEstadualRequestValidation : AbstractValidator<CheckPrestadorExistsByInscricaoEstadualRequest>
    {
        public CheckPrestadorExistsByInscricaoEstadualRequestValidation()
        {
            RuleFor(a => a.InscricaoEstadual)
                .MaximumLength(100)
                .WithMessage("A inscrição estadual não pode ter mais de 100 caracteres.");
        }
    }
}
