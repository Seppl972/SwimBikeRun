using Microsoft.EntityFrameworkCore;
using SwimBikeRun.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Windows;

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
        public string? Notiz { get; set; }
        [NotMapped] // wird nicht in DB gespeichert, sondern nur zur Berechnung in der App verwendet
        public double? DurchschnittsPace => PaceService.berechneFür(Sportart, DauerMinuten, DistanzKm);
    }

    // Enum für die Sportarten (für Dropdown in der UI)
    public enum SportartTyp
    {
        Schwimmen = 0,
        Radfahren = 1,
        Laufen = 2,
    }

}
