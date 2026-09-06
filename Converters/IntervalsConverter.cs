using SwimBikeRun.Models;
using System;
using System.Collections.Generic;

namespace SwimBikeRun.Converters // Ordner nicht vergessen == namespace
{
    public class IntervalsConverter
    {
        // Dictionary einmal als Feld definieren
        private readonly Dictionary<string, SportartTyp> _sportartMapping = new()
    {
        { "Run",  SportartTyp.Laufen },
        { "Ride", SportartTyp.Radfahren },
        { "Swim", SportartTyp.Schwimmen },
    };

        // Methode gibt eine Trainingseinheit zurück
        public Trainingseinheit Convert(IntervalsActivity activity)
        {
            return new Trainingseinheit
            {
                Datum = activity.start_date_local,
                DauerMinuten = activity.icu_recording_time / 60,  // Sekunden → Minuten
                DistanzKm = activity.distance / 1000,           // Meter → km
                Sportart = _sportartMapping.TryGetValue(activity.type ?? "", out var sportart)
                    ? sportart
                    : SportartTyp.Laufen // Fallback
            };
        }
    }
}
