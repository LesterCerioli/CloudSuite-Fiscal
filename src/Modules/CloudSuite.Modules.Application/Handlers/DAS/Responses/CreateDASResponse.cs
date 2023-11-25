using CloudSuite.Modules.Application.Core;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.DAS.Responses
{
    public class CreateDASResponse : Response
    {
        public Guid RequestId { get; private set; }

        public CreateDASResponse(Guid requestId, ValidationResult result)
        {
            RequestId = requestId;

            foreach (var item in result.Errors)
            {
                this.AddError(item.ErrorMessage);
            }
        }

        public CreateDASResponse(Guid requestId, string falseValidation)
        {
            RequestId = requestId;
            this.AddError(falseValidation);
        }
    }
}
