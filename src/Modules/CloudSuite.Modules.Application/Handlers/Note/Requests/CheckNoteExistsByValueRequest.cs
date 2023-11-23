using CloudSuite.Modules.Application.Handlers.Note.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Note.Requests
{
    public class CheckNoteExistsByValueRequest : IRequest<CheckNoteExistsByValueResponse>
    {

        public Guid Id { get; private set; }

        public decimal Value { get; set; }

        public CheckNoteExistsByValueRequest(decimal value)
        {
            Id = Guid.NewGuid();
            Value = value;
        }
    }
}
