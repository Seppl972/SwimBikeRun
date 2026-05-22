using System;
using System.Collections.Generic;
using System.Text;

// Später weitere Models ergänzen
namespace SwimBikeRun.Models
{
    // Zuerst Datenmodell definieren -> danach Data Access Layer -> dann UI bauen
    public class Trainingseinheit
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public string Name { get; set; }
        public int DauerMinuten { get; set; }
        public double DistanzKm { get; set; }
        public string? Notiz { get; set; } // ? bedeutet, dass Notiz optional ist (kann null sein)
    }
}
