using CloudSuite.Modules.Application.Core;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.CancelOrder.Responses
{
    public class CreateCancelOrderResponse : Response
    {
        public Guid RequestId { get; private set; }

        public CreateCancelOrderResponse(Guid requestId, ValidationResult result)
        {
            RequestId = requestId;

            foreach (var item in result.Errors)
            {
                this.AddError(item.ErrorMessage);
            }
        }

        public CreateCancelOrderResponse(Guid requestId, string falseValidation)
        {
            RequestId = requestId;
            this.AddError(falseValidation);
        }
    }
}
