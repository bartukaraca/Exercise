using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercise.Application.DTOs;

namespace Exercise.Application.Abstractions.Services
{
    public interface IRoadService
    {
        Task<RoadDto> SaveOrUpdateRoadAsync(RoadDto roadDto);


    }
}
