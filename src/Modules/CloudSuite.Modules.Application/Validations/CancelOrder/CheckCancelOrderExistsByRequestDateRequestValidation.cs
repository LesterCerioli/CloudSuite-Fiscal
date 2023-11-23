using CloudSuite.Modules.Application.Handlers.CancelOrder.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.CancelOrder
{
    public class CheckCancelOrderExistsByRequestDateRequestValidation : AbstractValidator<CheckCancelOrderExistsByRequestDateRequest>
    {
    }
}
