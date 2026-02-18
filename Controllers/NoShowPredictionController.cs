using Microsoft.AspNetCore.Mvc;
using Ratiu_Ruxandra_Proiect.Models;
using Ratiu_Ruxandra_Proiect.Services;
using System.Threading.Tasks;

namespace Ratiu_Ruxandra_Proiect.Controllers
{
    public class NoShowPredictionController : Controller
    {
        private readonly INoShowPredictionService _service;

        public NoShowPredictionController(INoShowPredictionService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new NoShowPredictionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(NoShowPredictionViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Construim inputul pentru API (FARA willShow)
            var input = new NoShowApiInput
            {
                patientAge = model.PatientAge,
                dayOfWeek = model.DayOfWeek,
                hour = model.Hour,
                doctorSpecialization = model.DoctorSpecialization,
                serviceName = model.ServiceName,

                hasReminder = model.HasReminder,
                previousNoShows = model.PreviousNoShows
            };

            var result = await _service.PredictAsync(input);

            if (result != null)
            {
                model.PredictedLabel = result.predictedLabel;

                if (result.score != null && result.score.Length >= 2)
                {
                    // IMPORTANT:
                    // score[0] = probabilitate pentru 1 (VINE)
                    // score[1] = probabilitate pentru 0 (NU VINE)

                    model.ProbabilityYes = result.score[0];
                    model.ProbabilityNo = result.score[1];
                }
            }

            return View(model);
        }
    }
}
