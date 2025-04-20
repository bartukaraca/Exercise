using System;
using System.Threading.Tasks;
using Exercise.Application.Abstractions.Services;
using Exercise.Application.DTOs;
using Exercise.Domain.Entities;
using Exercise.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Exercise.Persistence.Services
{
    public class RoadService : IRoadService
    {
        private readonly ExerciseDbContext _context;

        public RoadService(ExerciseDbContext context)
        {
            _context = context;
        }

        public async Task SaveOrUpdateRoadAsync(RoadDto roadDto)
        {
            // RoadStatus var mı kontrol et, yoksa ekle
            var existingRoadStatus = await _context.RoadStatuses
                .FirstOrDefaultAsync(rs => rs.Id == roadDto.RoadStatusId);

            if (existingRoadStatus == null)
            {
                var newRoadStatus = new RoadStatus { Id = roadDto.RoadStatusId };
                await _context.RoadStatuses.AddAsync(newRoadStatus);
                await _context.SaveChangesAsync();
                Console.WriteLine($"🚀 Yeni RoadStatus eklendi: RoadStatusId={roadDto.RoadStatusId}");
            }

            // Güncellenecek yolu bul
            var existingRoad = await _context.Roads
                .FirstOrDefaultAsync(r => r.Id == roadDto.Id);

            if (existingRoad != null)
            {
                // RoadStatusId güncelle
                existingRoad.RoadStatusId = roadDto.RoadStatusId;
                _context.Roads.Update(existingRoad);
                Console.WriteLine($"🔄 Yol güncellendi: Id={roadDto.Id}, Yeni RoadStatusId={roadDto.RoadStatusId}");
            }
            else
            {
                Console.WriteLine($"⚠️ Güncellenmek istenen yol bulunamadı: Id={roadDto.Id}");
                return;
            }

            await _context.SaveChangesAsync();
        }
    }
}
