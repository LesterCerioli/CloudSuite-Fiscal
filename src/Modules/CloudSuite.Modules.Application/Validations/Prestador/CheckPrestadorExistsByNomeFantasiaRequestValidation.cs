using CloudSuite.Modules.Application.Handlers.Prestador.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.Prestador
{
    public class CheckPrestadorExistsByNomeFantasiaRequestValidation : AbstractValidator<CheckPrestadorExistsByNomeFantasiaRequest>
    {
        public CheckPrestadorExistsByNomeFantasiaRequestValidation()
        {
            RuleFor(a => a.NomeFantasia)
                .NotNull()
                .WithMessage("O nome fantasia não pode ser nulo.")
                .MaximumLength(100)
                .WithMessage("O nome fantasia não pode ter mais de 100 caracteres.");
        }
    }
}
