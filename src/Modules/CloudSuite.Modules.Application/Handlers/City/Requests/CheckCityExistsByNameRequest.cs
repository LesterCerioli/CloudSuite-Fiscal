﻿using CloudSuite.Modules.Application.Handlers.City.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Application.Handlers.City.Requests
{
    public class CheckCityExistsByNameRequest : IRequest<CheckCityExistsByNameResponse>
    {

        public Guid Id { get; private set; }

        public string? CityName { get; set; }

        public CheckCityExistsByNameRequest(string cityName)
        {
            Id = Guid.NewGuid();
            CityName = cityName;
        }
    }
}
