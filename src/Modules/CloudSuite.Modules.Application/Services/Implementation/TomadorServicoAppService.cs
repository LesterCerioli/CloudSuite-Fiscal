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
    public class TomadorServicoAppService : ITomadorServicoAppService
    {
        private readonly ITomadorServicoRepository _tomadorServicoRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public TomadorServicoAppService(ITomadorServicoRepository tomadorServicoRepository, IMapper mapper, IMediatorHandler mediator)
        {
            _tomadorServicoRepository = tomadorServicoRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<TomadorServicoViewModel> GetByCnpj(Cnpj cnpj)
        {
            return _mapper.Map<TomadorServicoViewModel>(await _tomadorServicoRepository.GetByCnpj(cnpj));
        }

        public async Task<TomadorServicoViewModel> GetBySocialReason(string socialReason)
        {
            return _mapper.Map < TomadorServicoViewModel>(await _tomadorServicoRepository.GetBySocialReason(socialReason));
        }

        public async Task<TomadorServicoViewModel> Save(CreateTomadorServicoCommand createCommand)
        {
            return await _tomadorServicoRepository.Add(createCommand.GetEntity());
        }
    }
}
