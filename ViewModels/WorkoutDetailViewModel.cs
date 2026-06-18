using SwimBikeRun.Data;
using SwimBikeRun.Helpers;
using SwimBikeRun.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SwimBikeRun.ViewModels
{
    public class WorkoutDetailViewModel 
    {
        private readonly Action _zurückAction;

        // Direkt die Properties der Trainingseinheit
        public int Id { get; }
        public DateTime Datum { get; }
        public SportartTyp Sportart { get; }
        public int? DauerMinuten { get; }
        public double? DistanzKm { get; }
        public double? DurchschnittsPace { get; }
        public string? Notiz { get; }

        public ICommand ZurückCommand { get; }

        public WorkoutDetailViewModel(Trainingseinheit workout, Action zurückAction)
        {
            _zurückAction = zurückAction;  // ← nicht dbContext!

            // Werte vom Workout übernehmen
            Id = workout.Id;
            Datum = workout.Datum;
            Sportart = workout.Sportart;
            DauerMinuten = workout.DauerMinuten;
            DistanzKm = workout.DistanzKm;
            DurchschnittsPace = workout.DurchschnittsPace;
            Notiz = workout.Notiz;

            ZurückCommand = new RelayCommand(() => _zurückAction());
        }
    }
}
