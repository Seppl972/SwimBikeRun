using System;
using System.Windows.Input;

// Claude genertierter Code für vollständige MVVM-konformität sowie saubererer Architektur.
// Er ermöglicht die Bindung von UI-Elementen an Befehle in den ViewModels, ohne dass Code-Behind-Logik erforderlich ist.
// In diesem Fall wird ein einfacher RelayCommand implementiert, der eine Action ausführt, wenn der Befehl ausgeführt wird.
// Dies erleichtert die Trennung von Logik und UI und verbessert die Testbarkeit der Anwendung.
namespace SwimBikeRun.Helpers
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter) => _execute();
        public event EventHandler? CanExecuteChanged;
    }
}