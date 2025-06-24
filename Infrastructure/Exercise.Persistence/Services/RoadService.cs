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

        public async Task<RoadDto> SaveOrUpdateRoadAsync(RoadDto roadDto)
        {
            // Eğer roadDto.RoadStatusId gelmemiş ve default 99 ise, bu özel durumu yakalayacağız
            // Yani roadStatusId == 99 ise güncelleme yapılmayacak.

            // Önce existingRoad'u bulalım:
            var existingRoad = await _context.Roads
                .FirstOrDefaultAsync(r => r.Id == roadDto.Id);

            if (existingRoad != null)
            {
                // Güncelleme durumu
                if (roadDto.RoadStatusId != 99)
                {
                    // Önce RoadStatus kontrolü yap
                    var existingRoadStatus = await _context.RoadStatuses
                        .FirstOrDefaultAsync(rs => rs.Id == roadDto.RoadStatusId);

                    if (existingRoadStatus == null)
                    {
                        var newRoadStatus = new RoadStatus { Id = roadDto.RoadStatusId };
                        await _context.RoadStatuses.AddAsync(newRoadStatus);
                        await _context.SaveChangesAsync();
                        Console.WriteLine($"🚀 Yeni RoadStatus eklendi: RoadStatusId={roadDto.RoadStatusId}");
                    }

                    existingRoad.RoadStatusId = roadDto.RoadStatusId;
                    _context.Roads.Update(existingRoad);
                    Console.WriteLine($"🔄 Yol güncellendi: Id={roadDto.Id}, Yeni RoadStatusId={roadDto.RoadStatusId}");
                }
                else
                {
                    // roadDto.RoadStatusId == 99 ise güncelleme yapılmıyor, eski değer korunuyor
                    Console.WriteLine($"⚠️ RoadStatusId 99 olarak geldi, RoadStatus güncellenmedi: Id={roadDto.Id}");
                }
            }
            else
            {
                // Yeni yol ekleme
                // RoadStatusId 99 ise yeni yol için bile 99 atanabilir, ama kontrol isterseniz aşağıda ekleyebilirsiniz

                // RoadStatus kontrolü
                if (roadDto.RoadStatusId != 99)
                {
                    var existingRoadStatus = await _context.RoadStatuses
                        .FirstOrDefaultAsync(rs => rs.Id == roadDto.RoadStatusId);

                    if (existingRoadStatus == null)
                    {
                        var newRoadStatus = new RoadStatus { Id = roadDto.RoadStatusId };
                        await _context.RoadStatuses.AddAsync(newRoadStatus);
                        await _context.SaveChangesAsync();
                        Console.WriteLine($"🚀 Yeni RoadStatus eklendi: RoadStatusId={roadDto.RoadStatusId}");
                    }
                }

                var newRoad = new Road
                {
                    Id = roadDto.Id,
                    RoadStatusId = roadDto.RoadStatusId,
                };
                await _context.Roads.AddAsync(newRoad);
                Console.WriteLine($"🆕 Yeni yol eklendi: Id={roadDto.Id}, RoadStatusId={roadDto.RoadStatusId}");
            }

            await _context.SaveChangesAsync();

            var updatedRoad = await _context.Roads
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == roadDto.Id);

            return new RoadDto
            {
                Id = updatedRoad.Id,
                RoadStatusId = updatedRoad.RoadStatusId,
            };
        }

    }
}

    
