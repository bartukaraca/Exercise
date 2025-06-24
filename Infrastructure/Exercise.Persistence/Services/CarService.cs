using Exercise.Application.Abstractions.Services;
using Exercise.Application.DTOs;
using Exercise.Domain.Entities;
using Exercise.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise.Persistence.Services
{
    public class CarService : ICarService
    {
        private readonly ExerciseDbContext _context;

        public CarService(ExerciseDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UpdateCarRoadIdAsync(int carId, int roadId)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == carId);

                if (car == null)
                {
                    Console.WriteLine($"⚠️ Car bulunamadı. Car ID: {carId}");
                    return false;
                }

                car.RoadId = roadId;
                await _context.SaveChangesAsync();
                Console.WriteLine($"✅ Car güncellendi. Car ID: {carId}, Road ID: {roadId}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Car güncelleme hatası: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateCarRoadIdByVehicleNumberAsync(string vehicleNumber, int roadId)
        {
            try
            {
                var car = await _context.Cars
                    .FirstOrDefaultAsync(c => c.VehicleNumber == vehicleNumber);

                if (car == null)
                {
                    Console.WriteLine($"⚠️ Car bulunamadı. Vehicle Number: {vehicleNumber}");
                    return false;
                }

                car.RoadId = roadId;
                await _context.SaveChangesAsync();
                Console.WriteLine($"✅ Car güncellendi. Vehicle Number: {vehicleNumber}, Road ID: {roadId}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Car güncelleme hatası (Vehicle Number: {vehicleNumber}): {ex.Message}");
                return false;
            }
        }

        public async Task<CarDTO> GetCarByVehicleNumberAsync(string vehicleNumber)
        {
            try
            {
                var car = await _context.Cars
                    .FirstOrDefaultAsync(c => c.VehicleNumber == vehicleNumber);

                if (car == null)
                    return null;

                return new CarDTO
                {
                    Id = car.Id,
                    VehicleNumber = car.VehicleNumber,
                    RoadId = car.RoadId
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Car getirme hatası: {ex.Message}");
                return null;
            }
        }

        public async Task<CarDTO> GetCarByIdAsync(int carId)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == carId);

                if (car == null)
                    return null;

                return new CarDTO
                {
                    Id = car.Id,
                    VehicleNumber = car.VehicleNumber,
                    RoadId = car.RoadId
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Car getirme hatası: {ex.Message}");
                return null;
            }
        }

        public async Task<List<CarDTO>> GetAllCarsAsync()
        {
            try
            {
                var cars = await _context.Cars.ToListAsync();

                return cars.Select(c => new CarDTO
                {
                    Id = c.Id,
                    VehicleNumber = c.VehicleNumber,
                    RoadId = c.RoadId
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Cars getirme hatası: {ex.Message}");
                return new List<CarDTO>();
            }
        }

        public async Task<CarDTO> SaveCarAsync(CarDTO carDto)
        {
            try
            {
                Car car;

                if (carDto.Id > 0)
                {
                    // Güncelleme
                    car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == carDto.Id);
                    if (car == null)
                    {
                        Console.WriteLine($"⚠️ Güncellenecek car bulunamadı. ID: {carDto.Id}");
                        return null;
                    }
                    car.VehicleNumber = carDto.VehicleNumber;
                    car.RoadId = carDto.RoadId;
                }
                else
                {
                    // Yeni kayıt
                    car = new Car
                    {
                        VehicleNumber = carDto.VehicleNumber,
                        RoadId = carDto.RoadId
                    };
                    _context.Cars.Add(car);
                }

                await _context.SaveChangesAsync();

                return new CarDTO
                {
                    Id = car.Id,
                    VehicleNumber = car.VehicleNumber,
                    RoadId = car.RoadId
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Car kaydetme hatası: {ex.Message}");
                return null;
            }
        }
    }
}