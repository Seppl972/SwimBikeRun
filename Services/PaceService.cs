using SwimBikeRun.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwimBikeRun.Services
{
    public static class PaceService
    {
        public static double? berechneFür(SportartTyp sportart, int? dauer, double? distanz)
        {
            return sportart switch
            {
                SportartTyp.Laufen => distanz > 0 ? dauer / distanz : null,
                SportartTyp.Schwimmen => distanz > 0 ? (dauer / distanz) / 10 : null,
                SportartTyp.Radfahren => dauer > 0 ? distanz / ((double)dauer / 60) : null,
                _ => null
            };
        }
    }
}
