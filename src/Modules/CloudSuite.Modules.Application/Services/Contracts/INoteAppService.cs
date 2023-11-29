using CloudSuite.Modules.Application.Handlers.Note;
using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Contracts
{
    public interface INoteAppService
    {
        Task<NoteViewModel> GetByCnpj(Cnpj cnpj);

        Task<NoteViewModel> GetByNoteNumber(string noteNumber);

        Task<NoteViewModel> GetByValue(decimal value);

        Task<NoteViewModel> GetByEmissionDate(DateTime? date);

        Task Save(CreateNoteCommand createCommand);
    }
}
