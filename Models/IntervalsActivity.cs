using System;
using System.Collections.Generic;
using System.Text;

namespace SwimBikeRun.Models
{
    public class IntervalsActivity
    {
        public DateTime start_date_local { get; set; }
        public string? type { get; set; }        // "Run", "Ride", "Swim", "WeightTraining", "Yoga"
        public string? name { get; set; }        // Name der Aktivität
        public string? description { get; set; } // Beschreibung der Aktivität
        public int? icu_recording_time { get; set; }  // in Sekunden!
        public double? distance { get; set; }    // in Metern!
    }
}
