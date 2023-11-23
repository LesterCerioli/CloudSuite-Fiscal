using AutoMapper;
using CloudSuite.Modules.Application.Services.Contracts;
using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Contracts;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Implementation
{
    public class NoteAppService : INoteAppService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public NoteAppService(INoteRepository noteRepository, IMapper mapper, IMediatorHandler mediator)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<NoteViewModel> GetByCnpj(Cnpj cnpj)
        {
           return _mapper.Map<NoteViewModel>(await _noteRepository.GetByCnpj(cnpj));
        }

        public async Task<NoteViewModel> GetByEmissionDate(DateTime? date)
        {
            return _mapper.Map<NoteViewModel>(await _noteRepository.GetByEmissionDate(date));
        }

        public async Task<NoteViewModel> GetByNoteNumber(string noteNumber)
        {
            return _mapper.Map<NoteViewModel>(await _noteRepository.GetByNoteNumber(noteNumber));
        }

        public async Task<NoteViewModel> GetByValue(decimal value)
        {
            return _mapper.Map<NoteViewModel>(await _noteRepository.GetByValue(value));
        }

        public async Task<NoteViewModel> Save(createNoteCommand createCommand)
        {
            await _noteRepository.Add(createCommand.GetEntity());
        }
    }
}
