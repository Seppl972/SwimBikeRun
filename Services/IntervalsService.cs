using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Navigation;


namespace SwimBikeRun.Services
{
    public class IntervalsService
    {
        // 1. HttpClient als privates Feld der Klasse
        private readonly HttpClient _client;
        // statt hardcoded über Konfiguration:
        private readonly string _athleteId;



        // 2. Konstruktor setzt die Auth einmalig
        public IntervalsService(IConfiguration config)
        {
            _athleteId = config["Intervals:AthleteId"];
            _apiKey = config["Intervals:ApiKey"]; // nur eine Warning, kein Fehler
        }

        // 3. Methode für den GET Requestpublic async Task<string> GetIntervalsAsync()
        // async = diese Methode läuft asynchron, sie blockiert nicht die UI
        // Task<string> = die Methode gibt irgendwann einen string zurück (die JSON Antwort)
        // await = warte hier auf die API Antwort, aber friere die App nicht ein
        // Ohne async/await würde die App einfrieren bis Intervals.icu antwortet
        public async Task<string> GetIntervalsAsync()
        {
            var url = $"https://api.example.com/athletes/{_athleteId}/intervals";
            var response = await _client.GetAsync(url);
            return await response.Content.ReadAsStringAsync() ;
        }
    }
}
