using AutoMapper;
using CloudSuite.Modules.Application.Handlers.DAS;
using CloudSuite.Modules.Application.Services.Contracts;
using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Domain.Contracts;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Implementation
{
    public class DASAppService : IDASAppService
    {
        private readonly IDASRepository _dASRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public DASAppService(IDASRepository dASRepository, IMapper mapper, IMediatorHandler mediator)
        {
            _dASRepository = dASRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<DASViewModel> GetByDocumentNumber(string documentNumber)
        {
            return _mapper.Map<DASViewModel>(await _dASRepository.GetByDocumentNumber(documentNumber));
        }

        public async Task<DASViewModel> GetByDueDate(DateTime? dueDate)
        {
            return _mapper.Map<DASViewModel>(await _dASRepository.GetByDueDate(dueDate));
        }

        public async Task<DASViewModel> GetByReferenceMonth(string referenceMonth)
        {
            return _mapper.Map<DASViewModel>(await _dASRepository.GetByReferenceMonth(referenceMonth));
        }

        public async Task<DASViewModel> GetByReferenceYear(string referenceYear)
        {
            return _mapper.Map<DASViewModel>(await _dASRepository.GetByReferenceYear(referenceYear));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task Save(CreateDASCommand createCommand)
        {
            await _dASRepository.Add(createCommand.GetEntity());
        }
    }
}
