using CloudSuite.Modules.Application.Handlers.FederalTax.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.FederalTax
{
    public class CheckFederalTaxExistsByVinssRequestValidation : AbstractValidator<CheckFederalTaxExistsByVinssRequest>
    {
        public CheckFederalTaxExistsByVinssRequestValidation()
        {
            RuleFor(a => a.VINSS)
                .NotNull()
                .WithMessage("VPISSpecified não pode ser nulo.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor do VINSS deve ser maior ou igual a 0.");
        }
    }
}
