using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Contracts
{
    public interface IDASAppService
    {
        Task<DASViewModel> GetByReferenceMonth(string referenceMonth);

        Task<DASViewModel> GetByDueDate(DateTime dueDate);

        Task<DASViewModel> GetByDocumentNumber(string documentNumber);

        Task<DASViewModel> GetByReferenceYear(string referenceYear);

        Task<DASViewModel> Save(CreateDASCommand createCommand);
    }
}
