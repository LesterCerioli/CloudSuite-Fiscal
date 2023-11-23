using CloudSuite.Modules.Application.Handlers.Note.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Note.Requests
{
    public class CheckNoteExistsByNoteNumberRequest : IRequest<CheckNoteExistsByNoteNumberResponse>
    {

        public Guid Id { get; set; }

        public string? NoteNumber { get; private set; }

        public CheckNoteExistsByNoteNumberRequest(string? noteNumber)
        {
            Id = Guid.NewGuid();
            NoteNumber = noteNumber;
        }
    }
}
