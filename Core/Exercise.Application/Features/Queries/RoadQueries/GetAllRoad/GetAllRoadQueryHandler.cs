using Exercise.Application.Repositories;
using Exercise.Application.Repositories.RoadRepositories;
using Exercise.Application.Repositories.CarRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Features.Queries.RoadQueries.GetAllRoad
{
    public class GetAllRoadQueryHandler : IRequestHandler<GetAllRoadQueryRequest, GetAllRoadQueryResponse>
    {
        readonly IRoadReadRepository _roadReadRepository;
        readonly IRoadWriteRepository _roadWriteRepository;
        readonly ICarReadRepository _carReadRepository;

        public GetAllRoadQueryHandler(
            IRoadReadRepository roadReadRepository,
            IRoadWriteRepository roadWriteRepository,
            ICarReadRepository carReadRepository)
        {
            _roadReadRepository = roadReadRepository;
            _roadWriteRepository = roadWriteRepository;
            _carReadRepository = carReadRepository;
        }

        public async Task<GetAllRoadQueryResponse> Handle(GetAllRoadQueryRequest request, CancellationToken cancellationToken)
        {
            var roads = await _roadReadRepository.GetAll().Include(r => r.RoadStatus).ToListAsync();

            bool hasUpdates = false;

            // Her yol için araç sayısını kontrol et ve gerekirse durumu güncelle
            foreach (var road in roads)
            {
                int vehicleCount = await _carReadRepository.GetWhere(c => c.RoadId == road.Id).CountAsync();

                // Eğer araç sayısı 5'ten fazlaysa ve RoadStatusId 1 değilse güncelle
                if (vehicleCount > 5 && road.RoadStatusId != 1)
                {
                    road.RoadStatusId = 1; // Yoğun trafik durumu
                    hasUpdates = true;
                }
                // Eğer araç sayısı 5 veya daha azsa ve RoadStatusId 1 ise normal duruma çevir
                else if (vehicleCount <= 5 && road.RoadStatusId == 1)
                {
                    road.RoadStatusId = 2; // Normal trafik durumu (veya istediğiniz varsayılan durum)
                    hasUpdates = true;
                }
            }

            // Eğer güncelleme varsa kaydet
            if (hasUpdates)
            {
                await _roadWriteRepository.SaveAsync();
            }

            return new GetAllRoadQueryResponse
            {
                Message = "Yollar Başarıyla Getirildi.",
                Road = roads
            };
        }
    }
}