using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using SwimBikeRun.Data;
using SwimBikeRun.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SwimBikeRun.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly ApplicationDbContext _dbContext;

        // Wochenvolumen pro Sportart (in Stunden)
        public double SchwimmenStunden { get; private set; }
        public double RadfahrenStunden { get; private set; }
        public double LaufenStunden { get; private set; }
        public double GesamtStunden { get; private set; }
        public double WochenZiel => 10.0; // 10h/Woche Ziel

        // Fortschritt in Prozent für Progressbar
        public double FortschrittProzent =>
            Math.Min((GesamtStunden / WochenZiel) * 100, 100);

        // Aktuelle Kalenderwoche
        public string AktuelleWoche =>
            $"KW {System.Globalization.ISOWeek.GetWeekOfYear(DateTime.Today)} · {DateTime.Today.Year}";

        // Letztes Workout
        public string LetztesWorkout { get; private set; } = "–";

        // Deload-Erkennung
        public bool IstDeloadWoche { get; private set; }

        // LiveCharts Diagramm
        public ISeries[] Series { get; private set; } = Array.Empty<ISeries>();
        public Axis[] XAchse { get; private set; } = Array.Empty<Axis>();

        public DashboardViewModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            LadeDaten();
        }

        private void LadeDaten()
        {
            var heute = DateTime.Today;
            var wochenstart = heute.AddDays(-(int)heute.DayOfWeek + 1); // Montag
            var wochenende = wochenstart.AddDays(7);

            // Workouts dieser Woche
            var dieseWoche = _dbContext.Trainingseinheiten
                .Where(t => t.Datum >= wochenstart && t.Datum < wochenende)
                .ToList();

            // Volumen pro Sportart
            SchwimmenStunden = BerechneStunden(dieseWoche, SportartTyp.Schwimmen);
            RadfahrenStunden = BerechneStunden(dieseWoche, SportartTyp.Radfahren);
            LaufenStunden = BerechneStunden(dieseWoche, SportartTyp.Laufen);
            GesamtStunden = SchwimmenStunden + RadfahrenStunden + LaufenStunden;

            // Letztes Workout
            var letztes = _dbContext.Trainingseinheiten
                .OrderByDescending(t => t.Datum)
                .FirstOrDefault();

            if (letztes != null)
                LetztesWorkout = $"{letztes.Sportart} · {letztes.DistanzKm}km · {letztes.DauerMinuten}min";

            // Deload alle 4 Wochen erkennen
            int kw = System.Globalization.ISOWeek.GetWeekOfYear(heute);
            IstDeloadWoche = kw % 4 == 0;

            // Diagramm laden
            LadeVolumenDiagramm();
        }

        private double BerechneStunden(List<Trainingseinheit> workouts, SportartTyp sportart)
        {
            return workouts
                .Where(t => t.Sportart == sportart)
                .Sum(t => (t.DauerMinuten ?? 0) / 60.0);
        }

        private void LadeVolumenDiagramm()
        {
            var labels = new List<string>();
            var werte = new List<double>();

            // Letzte 8 Wochen
            for (int i = 7; i >= 0; i--)
            {
                var wochenstart = DateTime.Today
                    .AddDays(-(int)DateTime.Today.DayOfWeek + 1)
                    .AddDays(-7 * i);
                var wochenende = wochenstart.AddDays(7);

                int kw = System.Globalization.ISOWeek.GetWeekOfYear(wochenstart);
                labels.Add($"KW {kw}");

                double stunden = _dbContext.Trainingseinheiten
                    .Where(t => t.Datum >= wochenstart && t.Datum < wochenende)
                    .Sum(t => (t.DauerMinuten ?? 0) / 60.0);

                werte.Add(Math.Round(stunden, 1));
            }

            Series = new ISeries[]
            {
                new ColumnSeries<double>
                {
                    Name = "Stunden",
                    Values = werte.ToArray()
                }
            };

            XAchse = new Axis[]
            {
                new Axis { Labels = labels.ToArray() }
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}