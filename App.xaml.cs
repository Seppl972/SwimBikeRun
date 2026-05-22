using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SwimBikeRun.Data;
using System.Windows;

namespace SwimBikeRun
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            Services = serviceCollection.BuildServiceProvider();

            // MainWindow über DI erzeugen
            var mainWindow = Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // SQLite + EF Core
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=swimbikerun.db"));

            // Fenster registrieren
            services.AddTransient<MainWindow>();
        }
    }
}