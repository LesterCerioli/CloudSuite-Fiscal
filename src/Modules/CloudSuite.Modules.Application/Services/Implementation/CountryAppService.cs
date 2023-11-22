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
    public class CountryAppService : ICountryAppService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;


        public CountryAppService(
            ICountryRepository countryRepository,
            IMediatorHandler mediator,
            IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _mediator = mediator;

        }
        public async Task<CountryViewModel> GetbyCountryName(string countryName)
        {
            return _mapper.Map<CountryViewModel>(await _countryRepository.GetbyCountryName(countryName));
        }

        public async Task Save(CreateCountryCommand commandCreate)
        {
            return await _countryRepository.Add(commandCreate.GetEntity());
        }
    }
}
