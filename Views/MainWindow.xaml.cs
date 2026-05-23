using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using SwimBikeRun.Data;
using SwimBikeRun.Models;


namespace SwimBikeRun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApplicationDbContext _dbContext;

        public MainWindow(ApplicationDbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;

            // Datenbank beim Start anlegen/migrieren
            _dbContext.Database.EnsureCreated();

            // Testeintrag hinzufügen (nur wenn DB leer)
            if (!_dbContext.Trainingseinheiten.Any())
            {
                _dbContext.Trainingseinheiten.Add(new Trainingseinheit
                {
                    Id = 1,
                    Datum = DateTime.Today,
                    Sportart = "Run",
                    DauerMinuten = 60,
                    DistanzKm = 8.5,
                    Notiz = "Erstes Trainingseinheit - Testeintrag"
                });
                _dbContext.SaveChanges();
            }

            // ListView mit Daten aus der DB befüllen
            WorkoutsListView.ItemsSource = _dbContext.Trainingseinheiten.ToList();
        }
    }
}