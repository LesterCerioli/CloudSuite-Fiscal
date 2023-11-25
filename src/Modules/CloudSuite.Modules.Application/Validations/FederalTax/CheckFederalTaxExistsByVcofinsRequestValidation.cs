using CloudSuite.Modules.Application.Handlers.FederalTax.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.FederalTax
{
    public class CheckFederalTaxExistsByVcofinsRequestValidation : AbstractValidator<CheckFederalTaxExistsByVcofinsRequest>
    {
        public CheckFederalTaxExistsByVcofinsRequestValidation()
        {
            RuleFor(a => a.VCOFINS)
                .NotNull()
                .WithMessage("VPISSpecified não pode ser nulo.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor do VCOFINS deve ser maior ou igual a 0.");
        }
    }
}
