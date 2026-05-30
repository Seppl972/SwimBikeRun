using SwimBikeRun.Data;
using SwimBikeRun.ViewModels;
using System.Windows;               // Windows
using SwimBikeRun.Data;             // ApplicationDbContext
using SwimBikeRun.ViewModels;       // MainViewModel

namespace SwimBikeRun.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}