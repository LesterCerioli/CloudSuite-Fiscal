
using CloudSuite.Modules.Application.Handlers.fileUploads.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileUploadEntity = CloudSuite.Modules.Domain.Models.FileUpload;

namespace CloudSuite.Modules.Application.Handlers.fileUploads
{
    public class CreateFileUploadCommand : IRequest<CreateFileUploadResponse>
    {
        public Guid Id { get; private set; }

        public string? Name { get; set; }

        public string? Path { get; set;}

        public long? Size{ get; set; }

        public CreateFileUploadCommand()
        {
            Id = Guid.NewGuid();
        }

        public FileUploadEntity GetEntity()
        {
            return new FileUploadEntity(
                this.Name,
                this.Path,
                this.Size
            );
        }
    }
}
