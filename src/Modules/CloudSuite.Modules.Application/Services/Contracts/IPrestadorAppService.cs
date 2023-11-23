using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Common.ValueObjects;
using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Contracts
{
    public interface IPrestadorAppService
    {
        Task<PrestadorViewModel> GetByCnpj(Cnpj cnpj);

        Task<PrestadorViewModel> GetByInscricaoMunicipal(string inscricaoMunicipal);

        Task<PrestadorViewModel> GetByInscricaoEstadual(string inscricaoEstadual);

        Task<PrestadorViewModel> GetByDocTomadorEstrangeiro(string docTomadorEstrangeiro);

        Task<PrestadorViewModel> GetByNomeFantasia(string nomeFantasia);

        Task<PrestadorViewModel> Save(CreatePrestadorCommand createCommand);
    }
}
