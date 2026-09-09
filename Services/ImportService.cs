using SwimBikeRun.Converters;
using SwimBikeRun.Data;
using SwimBikeRun.Models;
using System.Collections.Generic;
using System.Text.Json;

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
            // 1. JSON holen
            var json = await _intervalsService.GetAktivitätenVonIntervalsAsync();

            // 2. Deserialisieren
            var activities = JsonSerializer.Deserialize<List<IntervalsActivity>>(json);

            if (activities == null) return;

            // 3. Konvertieren & speichern
            foreach (var activity in activities)
            {
                var einheit = _converter.Convert(activity);
                _dbContext.Trainingseinheiten.Add(einheit);
            }

            // 4. Einmal am Ende speichern
            _dbContext.SaveChanges();
        }
    }
}