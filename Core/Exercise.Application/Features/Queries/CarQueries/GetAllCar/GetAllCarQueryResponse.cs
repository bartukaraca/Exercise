using Exercise.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.CarQueries.GetAllCar
{
    public class GetAllCarQueryResponse
    {
        public string Message { get; set; }
        public List<Car> Car { get; set; }
    }
}
