using AutoMapper;
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
    public class DarfAppService : IDarfAppService
    {
        private readonly IDarfRepository _darfRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public DarfAppService(IDarfRepository darfRepository, IMapper mapper, IMediatorHandler mediator)
        {
            _darfRepository = darfRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<DarfViewModel> GetByDocumentNumber(string documentNumber)
        {
            return _mapper.Map<DarfViewModel>(await _darfRepository.GetByDocumentNumber(documentNumber));
        }

        public async Task<DarfViewModel> GetByDueDate(DateTime duedate)
        {
            return _mapper.Map<DarfViewModel>(await _darfRepository.GetByDueDate(duedate));
        }

        public async Task<DarfViewModel> GetByReferenceMonth(string referenceMonth)
        {
            return _mapper.Map<DarfViewModel>(await _darfRepository.GetByReferenceMonth(referenceMonth));
        }

        public async Task<DarfViewModel> GetByValidationDate(DateTime validationDate)
        {
            return _mapper.Map<DarfViewModel>(await _darfRepository.GetByValidationDate(validationDate));
        }

        public Task<DarfViewModel> Save(CreateDarfCommand createCommand)
        {
            throw new NotImplementedException();
        }
    }
}
