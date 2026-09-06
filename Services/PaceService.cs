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
                SportartTyp.Schwimmen => distanz > 0 && dauer > 0 ? (dauer / distanz) / 10 : null,
                SportartTyp.Radfahren => distanz > 0 && dauer > 0 ? distanz / ((double)dauer / 60) : null,
                SportartTyp.Laufen => distanz > 0 && dauer > 0 ? dauer / distanz : null,
                _ => null
            };
        }
    }
}
