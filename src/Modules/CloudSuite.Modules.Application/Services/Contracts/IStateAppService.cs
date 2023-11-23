using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Contracts
{
    public interface IStateAppService
    {
        Task<StateViewModel> GetByStateName(string stateName);

        Task<StateViewModel> GetByUF(string uf);

        Task<StateViewModel> Save(CreateStateCommand createCommand);
    }
}
