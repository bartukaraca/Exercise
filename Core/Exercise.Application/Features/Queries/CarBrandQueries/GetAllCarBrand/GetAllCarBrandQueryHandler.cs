using Exercise.Application.Repositories.CarBrandRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.CarBrandQueries.GetAllCarBrand
{
	public class GetAllCarBrandQueryHandler : IRequestHandler<GetAllCarBrandQueryRequest, GetAllCarBrandQueryResponse>
	{
		readonly ICarBrandReadRepository _carBrandReadRepository;

		public GetAllCarBrandQueryHandler(ICarBrandReadRepository carBrandReadRepository)
		{
			_carBrandReadRepository = carBrandReadRepository;
		}

		public async Task<GetAllCarBrandQueryResponse> Handle(GetAllCarBrandQueryRequest request, CancellationToken cancellationToken)
		{
		var carBrands = await _carBrandReadRepository.GetAll().ToListAsync();
			return new GetAllCarBrandQueryResponse
			{
				Success = true,
				Message = "Markalar Başarıyla Getirildi.",
				CarBrand = carBrands

			};
		}
	}
}
