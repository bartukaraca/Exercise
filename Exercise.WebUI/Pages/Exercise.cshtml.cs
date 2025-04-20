using Microsoft.AspNetCore.Mvc.RazorPages;
using Exercise.WebUI.Models;
using Exercise.WebUI.Services;

namespace Exercise.WebUI.Pages
{
    public class ExercisesModel : PageModel
    {
        private readonly ExerciseService _exerciseService;

        public ExercisesModel(ExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        public List<ExerciseDto> Exercises { get; set; } = new();

        public async Task OnGetAsync()
        {
            Exercises = await _exerciseService.GetAllExercisesAsync();
        }
    }
}
