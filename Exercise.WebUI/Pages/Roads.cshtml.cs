using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exercise.WebUI.Models;

namespace Exercise.WebUI.Pages
{
    public class RoadsModel : PageModel  // RoadsModel sýnýfý burada tanýmlý
    {
        private readonly RoadService _roadService;

        // Dependency Injection ile RoadService'i alýyoruz
        public RoadsModel(RoadService roadService)
        {
            _roadService = roadService;
        }

        public List<RoadDto> Roads { get; set; }

        public async Task OnGetAsync()
        {
            // API'den veriyi asenkron olarak alýyoruz
            var response = await _roadService.GetAllRoadsAsync();

            // Road verisini modelimize atýyoruz
            Roads = response.Road;
        }
    }
}
