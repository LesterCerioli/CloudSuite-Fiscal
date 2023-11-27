using AutoMapper;
using CloudSuite.Modules.Application.Handlers.Address;
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
    public class AddressAppService : IAddressAppService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;


        public AddressAppService(
            IAddressRepository addressRepository,
            IMediatorHandler mediator,
            IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _mediator = mediator;

        }

        public async Task<AddressViewModel> GetByAddressLine1(string addressLine1)
        {
            return _mapper.Map<AddressViewModel>(await _addressRepository.GetByAddressLine1(addressLine1));
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task Save(CreateAddressCommand commandCreate)
        {
             await _addressRepository.Add(commandCreate.GetEntity());
        }

    }
}
