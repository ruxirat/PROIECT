using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Ratiu_Ruxandra_Proiect.Grpc;

namespace Ratiu_Ruxandra_Proiect.Controllers
{
    public class PatientsGrpcController : Controller
    {
        private readonly string _grpcUrl = "https://localhost:7106";

        private GrpcChannel CreateChannel()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            return GrpcChannel.ForAddress(_grpcUrl, new GrpcChannelOptions { HttpHandler = handler });
        }

        public async Task<IActionResult> Index()
        {
            using var channel = CreateChannel();
            var client = new PatientService.PatientServiceClient(channel);

            var list = await client.GetAllAsync(new Empty());
            return View(list.Item);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string fullName, string phone, string email, string birthDate)
        {
            using var channel = CreateChannel();
            var client = new PatientService.PatientServiceClient(channel);

            await client.InsertAsync(new PatientMessage
            {
                FullName = fullName ?? "",
                Phone = phone ?? "",
                Email = email ?? "",
                BirthDate = birthDate ?? ""
            });

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            using var channel = CreateChannel();
            var client = new PatientService.PatientServiceClient(channel);

            await client.DeleteAsync(new PatientId { Id = id });
            return RedirectToAction(nameof(Index));
        }
    }
}
