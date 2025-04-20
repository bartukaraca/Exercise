using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exercise.WebUI.Models;

namespace Exercise.WebUI.Pages
{
    public class RoadsModel : PageModel  // RoadsModel s�n�f� burada tan�ml�
    {
        private readonly RoadService _roadService;

        // Dependency Injection ile RoadService'i al�yoruz
        public RoadsModel(RoadService roadService)
        {
            _roadService = roadService;
        }

        public List<RoadDto> Roads { get; set; }

        public async Task OnGetAsync()
        {
            // API'den veriyi asenkron olarak al�yoruz
            var response = await _roadService.GetAllRoadsAsync();

            // Road verisini modelimize at�yoruz
            Roads = response.Road;
        }
    }
}
