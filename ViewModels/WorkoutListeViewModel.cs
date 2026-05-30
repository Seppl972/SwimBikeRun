using System.Collections.ObjectModel;  // ObservableCollection
using SwimBikeRun.Data;                // ApplicationDbContext
using SwimBikeRun.Models;              // Trainingseinheitnamespace SwimBikeRun.ViewModels

namespace SwimBikeRun.ViewModels
{
    public class WorkoutListeViewModel
    {
        private readonly ApplicationDbContext _dbContext;

        public ObservableCollection<Trainingseinheit> Trainingseinheiten { get; set; }

        public WorkoutListeViewModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Trainingseinheiten = new ObservableCollection<Trainingseinheit>(
                _dbContext.Trainingseinheiten.ToList()
            );
        }
    }
}