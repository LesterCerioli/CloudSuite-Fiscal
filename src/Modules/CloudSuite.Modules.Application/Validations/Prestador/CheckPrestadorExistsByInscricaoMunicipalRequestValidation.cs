using CloudSuite.Modules.Application.Handlers.Prestador.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.Prestador
{
    public class CheckPrestadorExistsByInscricaoMunicipalRequestValidation : AbstractValidator<CheckPrestadorExistsByInscricaoMunicipalRequest>
    {
        public CheckPrestadorExistsByInscricaoMunicipalRequestValidation()
        {
            RuleFor(a => a.InscricaoMunicipal)
            .MaximumLength(100)
            .WithMessage("A inscrição municipal não pode ter mais de 100 caracteres.");
        }
    }
}
