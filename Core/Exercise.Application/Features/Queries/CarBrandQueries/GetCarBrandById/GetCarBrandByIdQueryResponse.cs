using Exercise.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.CarBrandQueries.GetCarBrandById
{
	public class GetCarBrandByIdQueryResponse
	{
        public string Message { get; set; }
        public bool Success { get; set; }
        public CarBrand CarBrand { get; set; }
    }
}
