using CloudSuite.Modules.Application.Handlers.District.Requests;
using CloudSuite.Modules.Application.Handlers.District.Responses;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.District
{
    public class CheckDistrictExistsByNameRequestValidation : AbstractValidator<CheckDistrictExistsByNameRequest>
    {
    }
}

