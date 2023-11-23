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
    public class PrestadorAppService : IPrestadorAppService
    {
        private readonly IPrestadorRepository _prestadorRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public PrestadorAppService(IPrestadorRepository prestadorRepository, IMapper mapper, IMediatorHandler mediator)
        {
            _prestadorRepository = prestadorRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<PrestadorViewModel> GetByCnpj(Cnpj cnpj)
        {
            return _mapper.Map<PrestadorViewModel>(await _prestadorRepository.GetByCnpj(cnpj));
        }

        public async Task<PrestadorViewModel> GetByDocTomadorEstrangeiro(string docTomadorEstrangeiro)
        {
            return _mapper.Map<PrestadorViewModel>(await _prestadorRepository.GetByDocTomadorEstrangeiro(docTomadorEstrangeiro));
        }

        public async Task<PrestadorViewModel> GetByInscricaoEstadual(string inscricaoEstadual)
        {
            return _mapper.Map<PrestadorViewModel>(await _prestadorRepository.GetByNomeFantasia(inscricaoEstadual));
        }

        public async Task<PrestadorViewModel> GetByInscricaoMunicipal(string inscricaoMunicipal)
        {
            return _mapper.Map<PrestadorViewModel>(await _prestadorRepository.GetByInscricaoEstadual(inscricaoMunicipal));
        }

        public async Task<PrestadorViewModel> GetByNomeFantasia(string nomeFantasia)
        {
            return _mapper.Map<PrestadorViewModel>(await _prestadorRepository.GetByNomeFantasia(nomeFantasia));
        }

        public async Task<PrestadorViewModel> Save(CreatePrestadorCommand createCommand)
        {
            await _prestadorRepository.Add(createCommand.GetEntity());
        }
    }
}
