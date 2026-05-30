using System.ComponentModel;        // INotifyPropertyChanged, PropertyChangedEventHandler
using System.Windows.Input;         // ICommand
using System.Windows;               // MessageBox
using SwimBikeRun.Data;             // ApplicationDbContext
using SwimBikeRun.Models;           // Trainingseinheit
using SwimBikeRun.Helpers;          // RelayCommand

namespace SwimBikeRun.ViewModels
{
    public class WorkoutAnlegenViewModel : INotifyPropertyChanged
    {
        private readonly ApplicationDbContext _dbContext;

        // Felder für das Formular
        public DateTime Datum { get; set; } = DateTime.Today;
        public string Sportart { get; set; } = "";
        public int DauerMinuten { get; set; }
        public double DistanzKm { get; set; }
        public string Notiz { get; set; } = "";


        public ICommand SpeichernCommand { get; }
        public ICommand ZurückCommand { get; }

        private readonly Action _zurückAction;

        public WorkoutAnlegenViewModel(ApplicationDbContext dbContext, Action zurueckAction)
        {
            _dbContext = dbContext;
            _zurückAction = zurueckAction;
            SpeichernCommand = new RelayCommand(Speichern);
            ZurückCommand = new RelayCommand(Zurück);
        }

        // Methode zum Speichern der neuen Trainingseinheit in der Datenbank
        private void Speichern()
        {
            var einheit = new Trainingseinheit
            {
                Datum = Datum,
                Sportart = Sportart,
                DauerMinuten = DauerMinuten,
                DistanzKm = DistanzKm,
                Notiz = Notiz
                // Id wird automatisch von EF vergeben!
            };

            _dbContext.Trainingseinheiten.Add(einheit);
            _dbContext.SaveChanges();

            MessageBox.Show("Workout gespeichert!");
        }

        // Methode zum Zurückkehren zur Listenansicht
        public void Zurück()
        {
            _zurückAction();
        }

        // ?INotifyPropertyChanged-Implementierung (wird benötigt, damit die UI auf Änderungen reagiert)
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}