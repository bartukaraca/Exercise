using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.CarBrandQueries.GetCarBrandById
{
	public class GetCarBrandByIdQueryRequest :IRequest<GetCarBrandByIdQueryResponse>
	{
        public string Id { get; set; }	
    }
}
