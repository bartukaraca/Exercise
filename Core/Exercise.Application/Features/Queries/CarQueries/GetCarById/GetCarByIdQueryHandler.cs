using Exercise.Application.Repositories.CarRepositories;
using Exercise.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.CarQueries.GetCarById
{
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQueryRequest, GetCarByIdQueryResponse>
    {
        readonly ICarReadRepository _carReadRepository;

        public GetCarByIdQueryHandler(ICarReadRepository carReadRepository)
        {
            _carReadRepository = carReadRepository;
        }

        public async Task<GetCarByIdQueryResponse> Handle(GetCarByIdQueryRequest request, CancellationToken cancellationToken)
        {
            Car car = await _carReadRepository.GetByIdAsync(request.Id, false);
            return new()
            {
                Message = "Ürün Getirildi.",
                Car = car,

            };
        }
    }
}
