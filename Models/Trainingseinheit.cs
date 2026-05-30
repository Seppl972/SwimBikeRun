using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

// Später weitere Models ergänzen
namespace SwimBikeRun.Models
{
    // Zuerst Datenmodell definieren -> danach Data Access Layer -> dann UI bauen
    public class Trainingseinheit
    {
        [Key] // Kennzeichnet die Id als Primärschlüssel für die Datenbank
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public string Sportart { get; set; }
        public int DauerMinuten { get; set; }
        public double DistanzKm { get; set; }
        public string? Notiz { get; set; }
    }
}
