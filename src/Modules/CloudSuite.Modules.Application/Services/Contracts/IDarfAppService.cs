using CloudSuite.Modules.Application.ViewModels;
using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Services.Contracts
{
    public interface IDarfAppService
    {
        Task<DarfViewModel> GetByReferenceMonth(string referenceMonth);

        Task<DarfViewModel> GetByDueDate(DateTime duedate);

        Task<DarfViewModel> GetByDocumentNumber(string documentNumber);

        Task<DarfViewModel> GetByValidationDate(DateTime validationDate);
                    
        Task<DarfViewModel> Save(CreateDarfCommand createCommand);
    }
}
