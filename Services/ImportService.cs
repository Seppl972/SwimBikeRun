using SwimBikeRun.Converters;
using SwimBikeRun.Data;
using SwimBikeRun.Models;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text.Json;
using System.Windows;

namespace SwimBikeRun.Services
{
    public class ImportService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IntervalsService _intervalsService;
        private readonly IntervalsConverter _converter;

        public ImportService(
            ApplicationDbContext dbContext,
            IntervalsService intervalsService,
            IntervalsConverter converter)
        {
            _dbContext = dbContext;
            _intervalsService = intervalsService;
            _converter = converter;
        }

        public async Task ImportiereAlleWorkoutsAsync()
        {
            int importiert = 0;
            int übersprungen = 0;

            // 1. JSON holen
            var json = await _intervalsService.GetAktivitätenVonIntervalsAsync();

            // 2. Deserialisieren
            var activities = JsonSerializer.Deserialize<List<IntervalsActivity>>(json);

            if (activities == null) return;

            // 3. Konvertieren & speichern
            foreach (var activity in activities)
            {
                // 3.1 Intervals API Format → SwimBikeRun Format
                //     "Run" → SportartTyp.Laufen
                //     3821 Meter → 3.821 km
                var einheit = _converter.Convert(activity);

                // 3.2 Auf Dublikate prüfen
                //     Any gibt true zurück wenn MINDESTENS EIN Element die Bedingung erfüllt
                bool existiertBereits = _dbContext.Trainingseinheiten.Any(t =>
                    t.Datum == einheit.Datum &&
                    t.DauerMinuten == einheit.DauerMinuten &&
                    t.Sportart == einheit.Sportart);
                // t    = jede Trainingseinheit in der DB
                // =>   = "für die gilt:"
                // t.Datum == einheit.Datum = das Datum muss übereinstimmen

                // 3.3 Nur hinzufügen, wenn  es noch nicht existiert
                if (!existiertBereits)
                {
                    _dbContext.Trainingseinheiten.Add(einheit);
                    importiert++;
                }
                else
                {
                    übersprungen++;
                }
            }

            // 4. Einmal am Ende speichern
            _dbContext.SaveChanges();

            // 5. Ergebnis ausgeben
            MessageBox.Show($"Import abgeschlossen!\n\n{importiert} " +
                            $"Trainingseinheiten importiert.\n{übersprungen} " +
                            $"Trainingseinheiten übersprungen (bereits vorhanden).", 
                            "Import abgeschlossen");
        }


    }
}