﻿using CloudSuite.Modules.Application.Handlers.Darf.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.Darf.Requests
{
    public class CheckDarfExistsByDocumentNumberRequest : IRequest<CheckDarfExistsByDocumentNumberResponse>
    {

        public Guid Id { get; private set; }

        public string? DocumentNumber { get; set; }

        public CheckDarfExistsByDocumentNumberRequest(string?documentNumber)
        {
            Id = Guid.NewGuid();
            DocumentNumber = documentNumber;
        }

    }
}
