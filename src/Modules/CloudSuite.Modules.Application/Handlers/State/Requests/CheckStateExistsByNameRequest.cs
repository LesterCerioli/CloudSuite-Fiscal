using CloudSuite.Modules.Application.Handlers.State.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.State.Requests
{
    public class CheckStateExistsByNameRequest : IRequest<CheckStateExistsByNameResponse>
    {

        public Guid Id { get; private set; }

        public string? StateName { get; private set; }

        public CheckStateExistsByNameRequest(Guid id, string? stateName)
        {
            Id = Guid.NewGuid();
            StateName = stateName;
        }

    }
}
