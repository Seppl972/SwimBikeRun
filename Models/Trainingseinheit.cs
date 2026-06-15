using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public SportartTyp Sportart { get; set; }
        public int DauerMinuten { get; set; }
        public double DistanzKm { get; set; }
        public string? Notiz { get; set; }

        [NotMapped] // Dieses Feld wird nicht in der Datenbank gespeichert, da es berechnet wird
        public double? DurchschnittsPace { get; set; }

        // Konstruktor soll die Berechnung beim Erstellen einer Trainingseinheit berechnen
        // Erstmal getrennt, später vielleicht in einer Methode, die je nach Sportart die richtige Berechnung anstößt
        public Trainingseinheit()
        {
            berechneDurchschnittsPace();
            DurchschnittsPace = berechneDurchschnittsPace();
        }

        public double berechneDurchschnittsPace()
        {
            double pace = 0;
            berechneDurchschnittLaufen();
            berechneDurchschnittRadfahren();
            berechneDurchschnittSchwimmen();
            return pace;
        }
        public void berechneDurchschnittLaufen()
        {
            double paceLaufen = (DauerMinuten / DistanzKm);
            double pace = paceLaufen;
        }
        public void berechneDurchschnittRadfahren()
        {
            double paceRadfahren = DauerMinuten / DistanzKm;
            double pace = paceRadfahren;
        }
        public double berechneDurchschnittSchwimmen()
        {
            double paceSchwimmen = DauerMinuten / DistanzKm;
            double DurchschnittsPace = paceSchwimmen;
            return paceSchwimmen;
        }
    }

    // Enum für die Sportarten (für Dropdown in der UI)
    public enum SportartTyp
    {
        Schwimmen = 0,
        Radfahren = 1,
        Laufen = 2,
    }

}
