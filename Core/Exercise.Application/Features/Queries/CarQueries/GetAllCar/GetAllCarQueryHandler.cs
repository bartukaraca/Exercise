using Exercise.Application.Repositories;
using Exercise.Application.Repositories.CarRepositories;
using Exercise.Domain.Entities;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.CarQueries.GetAllCar
{
    public class GetAllCarQueryHandler : IRequestHandler<GetAllCarQueryRequest, GetAllCarQueryResponse>
    {
        readonly ICarReadRepository _carReadRepository;

        public GetAllCarQueryHandler(ICarReadRepository carReadRepository)
        {
            _carReadRepository = carReadRepository;
        }

        public async Task<GetAllCarQueryResponse> Handle(GetAllCarQueryRequest request, CancellationToken cancellationToken)
        {
            var cars = _carReadRepository.GetAll().ToList();
            return new()
            {
                Message = "Basarili",
                Car = cars
            };
        }
    }
}
