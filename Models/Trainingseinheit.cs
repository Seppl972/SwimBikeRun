using System;
using System.Collections.Generic;
using System.Text;

// Später weitere Models ergänzen
namespace SwimBikeRun.Models
{
    class Trainingseinheit
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public string NameTrainingseinheit { get; set; }
        public string Beschreibung { get; set; }
    }
}
