using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Ratiu_Ruxandra_Proiect.Models;

namespace Ratiu_Ruxandra_Proiect.Services
{
    public class NoShowPredictionService : INoShowPredictionService
    {
        private readonly HttpClient _httpClient;

        public NoShowPredictionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<NoShowApiResponse?> PredictAsync(NoShowApiInput input)
        {
            var response = await _httpClient.PostAsJsonAsync("/predict", input);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<NoShowApiResponse>();
        }
    }
}
