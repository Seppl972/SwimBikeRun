using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using SwimBikeRun.Helpers;
using System.ComponentModel;        // INotifyPropertyChanged, PropertyChangedEventHandler
using System.Windows.Input;         // ICommand
using SwimBikeRun.Data;             // ApplicationDbContext
using SwimBikeRun.Helpers;          // RelayCommand
using System.Windows;               // ← für MessageBox



namespace SwimBikeRun.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ApplicationDbContext _dbContext;

        public MainViewModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();    // Datenbank beim Start anlegen/migrieren
            AktuelleView = new WorkoutListeViewModel(_dbContext);    // Standardmäßig die Listenansicht anzeigen

            NeuCommand = new RelayCommand(OeffneWorkoutAnlegen);
            AnzeigenCommand = new RelayCommand(OeffneWorkoutDetail);
        }

        // AktuelleView ist die Eigenschaft, die die aktuell angezeigte View steuert
        private object _aktuelleView;
        public object AktuelleView
        {
            get { return _aktuelleView; }
            set 
            { 
                _aktuelleView = value;
                OnPropertyChanged(nameof(AktuelleView));
            }
        }

        public ICommand NeuCommand { get; }
        public ICommand AnzeigenCommand { get; }
        public ICommand LöschenCommand => new RelayCommand(() =>
            {
                if (AktuelleView is WorkoutListeViewModel listeViewModel)
                {
                    listeViewModel.LöschenCommand.Execute(null);
                }
                else
                {
                    MessageBox.Show("Löschen ist vorerst nur in der Listenansicht möglich.");
                }
            });

        public ICommand BearbeitenCommand => new RelayCommand(() =>
        {
            if (AktuelleView is WorkoutListeViewModel listeViewModel)
            {
                var ausgewählt = listeViewModel.AusgewähltesWorkout;

                if (ausgewählt == null)
                {
                    MessageBox.Show("Bitte erst ein Workout in der Liste auswählen!");
                    return; // ← ohne das crasht es!
                }

                AktuelleView = new WorkoutBearbeitenViewModel(_dbContext, ausgewählt, ZurückZurListe);
            }
        });

        private void OeffneWorkoutAnlegen()
        {
            AktuelleView = new WorkoutAnlegenViewModel(_dbContext, ZurückZurListe); // dbContext weitergeben, damit die Anlegen-View auch Zugriff auf die Datenbank hat            
        }

        private void OeffneWorkoutDetail()
        {
            if (AktuelleView is WorkoutListeViewModel listeViewModel)
            {
                var ausgewählt = listeViewModel.AusgewähltesWorkout;

                if (ausgewählt == null)
                {
                    MessageBox.Show("Bitte erst ein Workout auswählen!");
                    return;
                }

                AktuelleView = new WorkoutDetailViewModel(ausgewählt, ZurückZurListe);
            }
        }


        private void ZurückZurListe()
        {
            AktuelleView = new WorkoutListeViewModel(_dbContext); // Zurück zur Listenansicht wechseln
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
