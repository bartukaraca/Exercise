using Exercise.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.CarBrandQueries.GetAllCarBrand
{
	public class GetAllCarBrandQueryResponse 
    {
        public string Message { get; set; }
        public bool Success { get; set; }
		public string Name { get; set; }
		public string Country { get; set; }
        public List<CarBrand> CarBrand { get; set; }

    }
}
