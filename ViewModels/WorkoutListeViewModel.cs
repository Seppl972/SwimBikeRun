using SwimBikeRun.Data;                // ApplicationDbContext
using SwimBikeRun.Helpers;
using SwimBikeRun.Models;              // Trainingseinheitnamespace SwimBikeRun.ViewModels
using System.Collections.ObjectModel;  // ObservableCollection
using System.Windows;
using System.Windows.Input;

namespace SwimBikeRun.ViewModels
{
    public class WorkoutListeViewModel
    {
        private readonly ApplicationDbContext _dbContext;

        public ObservableCollection<Trainingseinheit> Trainingseinheiten { get; set; }

        // Merker, welches Workout angeklickt wurde
        private Trainingseinheit? _ausgewähltesWorkout;
        public Trainingseinheit? AusgewähltesWorkout
        {
            get => _ausgewähltesWorkout; 
            set
            {
                _ausgewähltesWorkout = value;
            }
 
        }

        public WorkoutListeViewModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Trainingseinheiten = new ObservableCollection<Trainingseinheit>(
                _dbContext.Trainingseinheiten.ToList()

            );
            LöschenCommand = new RelayCommand(Löschen);
        }

        public ICommand LöschenCommand { get; }

        // Methode zum Löschen einer Trainingseinheit in der Datenbank
        public void Löschen()
        {
            if (AusgewähltesWorkout == null)
            {
                MessageBox.Show("Bitte wähle ein Workout aus, um es zu löschen.");
                return;
            }
            _dbContext.Trainingseinheiten.Remove(AusgewähltesWorkout);
            _dbContext.SaveChanges();
            Trainingseinheiten.Remove(AusgewähltesWorkout); // Auch aus der ObservableCollection entfernen, damit die UI aktualisiert wird
            AusgewähltesWorkout = null; // Auswahl zurücksetzen (Schutzmaßnahme damit dein null-Check weiter oben seinen Job machen kann)

            // MessageBox.Show("Workout gelöscht!");
        }

    }
}