using Microsoft.EntityFrameworkCore;
using SwimBikeRun.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Windows;
using System.Windows.Navigation;

// Später weitere Models ergänzen
namespace SwimBikeRun.Models
{
    // Zuerst Datenmodell definieren -> danach Data Access Layer -> dann UI bauen
    public class Trainingseinheit
    {
        [Key] // Kennzeichnet die Id als Primärschlüssel für die Datenbank
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public SportartTyp Sportart { get; set; }
        public int? DauerMinuten { get; set; }
        public double? DistanzKm { get; set; }
        public string? Titel { get; set; }
        public string? Beschreibung { get; set; }
        [NotMapped] // wird nicht in DB gespeichert, sondern nur zur Berechnung in der App verwendet
        public double? DurchschnittsPace => PaceService.berechneFür(Sportart, DauerMinuten, DistanzKm);
        [NotMapped]
        public string? DurchschnittsPaceFormatiert
        {
            get
            {
                // 1. Durschnittspace auf null prüfen
                if (DurchschnittsPace == null) return "-";

                // 2. switch auf Sportart
                return Sportart switch
                {
                    // 3. Laufen/Schwimmen → FormatierePace() + Einheit
                    SportartTyp.Laufen => FormatierePace(DurchschnittsPace.Value) + " min/km",
                    SportartTyp.Schwimmen => FormatierePace(DurchschnittsPace.Value) + " min/100m",
                    // 4. Radfahren → km/h
                    SportartTyp.Radfahren => $"{DurchschnittsPace.Value:F1} km/h",
                    // 5. Rest → "–"
                    _ => "-"
                };
            }
        }

        private string FormatierePace(double pace)
        {
            // Minuten und Sekunden berechnen
            int minuten = (int)pace;
            int sekunden = (int)((pace - minuten) * 60);
            return $"{minuten}:{sekunden:D2}";
        }
    }

    // Enum für die Sportarten (für Dropdown in der UI)
    public enum SportartTyp
    {
        Schwimmen = 0,
        Radfahren = 1,
        Laufen = 2,
        Krafttraining = 3,
        Yoga = 4,
    }
}
