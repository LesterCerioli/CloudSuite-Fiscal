using CloudSuite.Modules.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Domain.Contracts
{
    public interface IFileUploadRepository
    {
        Task<FileUpload> GetByName(string name);

        Task<FileUpload> GetByPath(string path);

        Task<IEnumerable<FileUpload>> GetList();

        Task Add(FileUpload upload);

        void Update(FileUpload upload);

        void Remove(FileUpload upload);

    }
}
