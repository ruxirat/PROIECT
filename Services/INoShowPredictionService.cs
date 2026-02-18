using System.Threading.Tasks;
using Ratiu_Ruxandra_Proiect.Models;

namespace Ratiu_Ruxandra_Proiect.Services
{
    public interface INoShowPredictionService
    {
        Task<NoShowApiResponse?> PredictAsync(NoShowApiInput input);
    }
}
