using Exercise.Application.Features.Queries.CarQueries.GetCarById;
using Exercise.Application.Repositories.CarBrandRepositories;
using Exercise.Application.Repositories.CarRepositories;
using Exercise.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.CarBrandQueries.GetCarBrandById
{
	public class GetCarBrandByIdQueryHandler : IRequestHandler<GetCarBrandByIdQueryRequest, GetCarBrandByIdQueryResponse>
	{
		readonly ICarBrandReadRepository _carBrandReadRepository;

		public GetCarBrandByIdQueryHandler(ICarBrandReadRepository carBrandReadRepository)
		{
			_carBrandReadRepository = carBrandReadRepository;
		}

		public async Task<GetCarBrandByIdQueryResponse> Handle(GetCarBrandByIdQueryRequest request, CancellationToken cancellationToken)
		{
		 CarBrand carbrand	=	await _carBrandReadRepository.GetByIdAsync(request.Id,false);
			return new()
			{
				CarBrand = carbrand,
				Message = "Marka Başarıyla Getirildi",
				Success = true
			};
		}
	}
}
