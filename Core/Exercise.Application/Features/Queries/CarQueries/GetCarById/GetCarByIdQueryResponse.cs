using Exercise.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.CarQueries.GetCarById
{
    public class GetCarByIdQueryResponse
    {
        public string Message { get; set; }
        public Car Car { get; set; }
    }
}
