using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Windows.Navigation;


namespace SwimBikeRun.Services
{
    public class IntervalsService
    {
        // 1. HttpClient als privates Feld der Klasse
        private readonly HttpClient _client;
        private readonly string _athleteId = "i643774";
        private readonly string _APIKey = "5zx0uuxhrd10ulb7np8amcfi3";

        // 2. Konstruktor setzt die Auth einmalig
        public IntervalsService()
        {
            _client = new HttpClient();

            var credentials = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"API_KEY:{_APIKey}"));

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", credentials);
        }

        // 3. Methode für den GET Requestpublic async Task<string> GetIntervalsAsync()
        public async Task<string> GetIntervalsAsync()
        {
            var url = $"https://api.example.com/athletes/{_athleteId}/intervals";
            var response = await _client.GetAsync(url);
            return await response.Content.ReadAsStringAsync() ;
        }
    }
}
