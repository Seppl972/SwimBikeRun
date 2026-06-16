using SwimBikeRun.Data;
using SwimBikeRun.Helpers;
using SwimBikeRun.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace SwimBikeRun.ViewModels
{
    public class WorkoutBearbeitenViewModel : INotifyPropertyChanged
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Trainingseinheit _zuBearbeitendesWorkout;
        private readonly Action _zurückAction;

        // Felder vorausgefüllt mit bestehendem Workout
        public DateTime Datum { get; set; }
        public SportartTyp Sportart { get; set; }
        public int? DauerMinuten { get; set; }
        public double? DistanzKm { get; set; }
        public string? Notiz { get; set; }

        public ICommand SpeichernCommand { get; }
        public ICommand ZurückCommand { get; }

        public WorkoutBearbeitenViewModel(ApplicationDbContext dbContext, Trainingseinheit workout, Action zurückAction)
        {
            _dbContext = dbContext;
            _zuBearbeitendesWorkout = workout;
            _zurückAction = zurückAction;

            // Felder mit bestehendem Workout vorausfüllen
            Datum = workout.Datum;
            Sportart = workout.Sportart;
            DauerMinuten = workout.DauerMinuten;
            DistanzKm = workout.DistanzKm;
            Notiz = workout.Notiz;

            SpeichernCommand = new RelayCommand(Speichern);
            ZurückCommand = new RelayCommand(Zurück);
        }

        private void Speichern()
        {
            // Bestehendes Workout aktualisieren
            _zuBearbeitendesWorkout.Datum = Datum;
            _zuBearbeitendesWorkout.Sportart = Sportart;
            _zuBearbeitendesWorkout.DauerMinuten = DauerMinuten;
            _zuBearbeitendesWorkout.DistanzKm = DistanzKm;
            _zuBearbeitendesWorkout.Notiz = Notiz;

            _dbContext.SaveChanges(); // ← kein Add() nötig, nur speichern!
            _zurückAction();
        }

        private void Zurück()
        {
            _zurückAction();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
