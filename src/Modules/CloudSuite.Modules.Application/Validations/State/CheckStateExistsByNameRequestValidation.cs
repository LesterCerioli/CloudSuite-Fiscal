﻿using CloudSuite.Modules.Application.Handlers.State.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Validations.State
{
    public class CheckStateExistsByNameRequestValidation : AbstractValidator<CheckStateExistsByNameRequest>
    {
    }
}
