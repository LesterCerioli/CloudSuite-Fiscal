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
    public class DistrictAppService : IDistrictAppService
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public async Task<DistrictViewModel> GetByName(string name)
        {
            return _mapper.Map<DistrictViewModel>( await _districtRepository.GetByName(name));
        }

        public async Task<DistrictViewModel> Save(CreateDistrictCommand createCommand)
        {
            await _districtRepository.Add(createCommand.GetEntity());
        }
    }
}
