using Exercise.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercise.Application.Abstractions.Services
{
    public interface ICarService
    {
        // Mevcut methodlarınız...
        Task<CarDTO> GetCarByIdAsync(int carId);
        Task<List<CarDTO>> GetAllCarsAsync();
        Task<CarDTO> SaveCarAsync(CarDTO carDto);

        // Car'ın roadId'sini güncelleme methodları
        Task<bool> UpdateCarRoadIdAsync(int carId, int roadId);
        Task<bool> UpdateCarRoadIdByVehicleNumberAsync(string vehicleNumber, int roadId);

        // Araç numarası ile car bulma
        Task<CarDTO> GetCarByVehicleNumberAsync(string vehicleNumber);
    }
}