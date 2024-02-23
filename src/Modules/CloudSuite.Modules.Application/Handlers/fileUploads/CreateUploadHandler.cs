using CloudSuite.Modules.Application.Handlers.fileUploads.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.fileUploads
{
    public class CreateUploadHandler : IRequestHandler<CreateFileUploadCommand, CreateFileUploadResponse>
    {
        public Task<CreateFileUploadResponse> Handle(CreateFileUploadCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
