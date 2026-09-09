using System;
using System.Collections.Generic;
using System.Text;

namespace SwimBikeRun.Models
{
    public class IntervalsActivity
    {
        public DateTime start_date_local { get; set; }
        public string? type { get; set; }        // "Run", "Ride", "Swim", "WeightTraining", "Yoga"
        public int? icu_recording_time { get; set; }  // in Sekunden!
        public double? distance { get; set; }    // in Metern!
    }
}
