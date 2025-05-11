using Microsoft.AspNetCore.Mvc.RazorPages;
using Exercise.WebUI.Models;
using Exercise.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

public class CarModel : PageModel
{
    private readonly CarService _carService;

    public CarModel(CarService carService)
    {
        _carService = carService;
    }

    public List<CarDTO> Cars { get; set; }

    public async Task OnGetAsync()
    {
        Cars = await _carService.GetAllCarsAsync();
    }
    public async Task<IActionResult> OnPostDeleteAsync(int carId)
    {
        var success = await _carService.DeleteCarAsync(carId);

        if (success)
        {
            TempData["Message"] = "Ara� ba�ar�yla silindi.";
        }
        else
        {
            TempData["Message"] = "Ara� silinemedi.";
        }

        return RedirectToPage();
    }

}
