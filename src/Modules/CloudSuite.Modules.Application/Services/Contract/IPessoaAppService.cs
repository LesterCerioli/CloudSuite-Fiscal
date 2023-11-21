using CloudSuite.Modules.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Contract
{
    public interface IPessoaAppService 
    {
        Task<PessoaViewModel> GetByName(string name);


        Task Save(PessoaViewModel commandCreate);

    }
}
