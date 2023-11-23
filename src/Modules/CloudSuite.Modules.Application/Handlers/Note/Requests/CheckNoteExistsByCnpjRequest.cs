using CloudSuite.Modules.Application.Handlers.Note.Responses;
using CloudSuite.Modules.Common.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Note.Requests
{
    public class CheckNoteExistsByCnpjRequest : IRequest<CheckNoteExistsByCnpjResponse>
    {

        public Guid Id { get; set; }

        public Cnpj Cnpj { get; set; }

        public CheckNoteExistsByCnpjRequest(Guid id, Cnpj cnpj)
        {
            Id = Guid.NewGuid();
            Cnpj = cnpj;
        }
    }
}
