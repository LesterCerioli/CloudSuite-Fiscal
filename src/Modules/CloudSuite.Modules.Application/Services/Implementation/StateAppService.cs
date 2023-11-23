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
    public class StateAppService : IStateAppService
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public StateAppService(IStateRepository stateRepository, IMapper mapper, IMediatorHandler mediator)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<StateViewModel> GetByStateName(string stateName)
        {
            return _mapper.Map<StateViewModel>(await _stateRepository.GetByStateName(stateName))a;
        }

        public async Task<StateViewModel> GetByUF(string uf)
        {
            return _mapper.Map<StateViewModel>(await _stateRepository.GetByUF(uf));
        }

        public async Task<StateViewModel> Save(CreateStateCommand createCommand)
        {
            await _stateRepository.Add(createCommand.GetEntity());
        }
    }
}
