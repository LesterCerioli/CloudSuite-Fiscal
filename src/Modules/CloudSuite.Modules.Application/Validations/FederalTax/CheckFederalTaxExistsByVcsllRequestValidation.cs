using CloudSuite.Modules.Application.Handlers.FederalTax.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.FederalTax
{
    public class CheckFederalTaxExistsByVcsllRequestValidation : AbstractValidator<CheckFederalTaxExistsByVcsllRequest>
    {
        public CheckFederalTaxExistsByVcsllRequestValidation()
        {
            RuleFor(a => a.VCSLL)
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor do VCSLL deve ser maior ou igual a 0.");
        }
    }
}
