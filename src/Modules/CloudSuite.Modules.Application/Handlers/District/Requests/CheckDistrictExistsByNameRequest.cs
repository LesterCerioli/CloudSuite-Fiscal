using CloudSuite.Modules.Application.Handlers.District.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.District.Requests
{
    public class CheckDistrictExistsByNameRequest : IRequest<CheckDistrictExistsByNameResponse>
    {
  
        public Guid Id { get; set; }

        public string? Name { get; private set; }

        public CheckDistrictExistsByNameRequest(string? name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
