using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SwimBikeRun.Services
{
    public class IntervalsService
    {
        private readonly HttpClient _client;
        private readonly string _athleteId;

        public IntervalsService(IConfiguration config)
        {
            _athleteId = config["Intervals:AthleteId"] ?? "";
            var apiKey = config["Intervals:ApiKey"] ?? "";

            _client = new HttpClient();

            // Basic Auth Header setzen
            var credentials = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"API_KEY:{apiKey}"));
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", credentials);
        }

        public async Task<string> GetAktivitätenVonIntervalsAsync()
        {
            var url = $"https://intervals.icu/api/v1/athlete/{_athleteId}/activities?oldest=2026-01-01";
            var response = await _client.GetAsync(url); // <- das ist der HTTP GET
            return await response.Content.ReadAsStringAsync();
        }
    }
}