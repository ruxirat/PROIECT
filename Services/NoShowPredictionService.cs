using Grpc.Net.Client;
using Ratiu_Ruxandra_Proiect.Models;
using System.Threading.Tasks;
using GrpcService1;

namespace Ratiu_Ruxandra_Proiect.Services
{
    public class NoShowPredictionService : INoShowPredictionService
    {
        private readonly GrpcChannel _channel;

        public NoShowPredictionService()
        {
            _channel = GrpcChannel.ForAddress("https://localhost:7051");
        }

        public async Task<NoShowApiResponse?> PredictAsync(NoShowApiInput input)
        {
            var client = new NoShowPredictor.NoShowPredictorClient(_channel);

            var reply = await client.PredictAsync(new NoShowRequest
            {
                PatientAge = input.patientAge,
                DayOfWeek = input.dayOfWeek,
                Hour = input.hour,
                DoctorSpecialization = input.doctorSpecialization,
                ServiceName = input.serviceName,
                HasReminder = input.hasReminder,
                PreviousNoShows = input.previousNoShows
            });

            return new NoShowApiResponse
            {
                predictedLabel = reply.PredictedLabel,
                score = new float[] { reply.ProbabilityYes, reply.ProbabilityNo }
            };
        }
    }
}
