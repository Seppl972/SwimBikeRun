# 🏊 🚴 🏃 SwimBikeRun

Eine WPF-Desktopanwendung zur Verwaltung von Triathlon-Trainingseinheiten – gebaut mit C#, WPF und Entity Framework.

---

## 📋 Über das Projekt

SwimBikeRun ermöglicht es Triathleten, ihre Trainingseinheiten strukturiert zu erfassen, zu verwalten und auszuwerten. Die Anwendung folgt dem MVVM-Architekturmuster und nutzt eine lokale Datenbank zur persistenten Speicherung.

---

## 🛠️ Technologien

| Technologie | Verwendung |
|---|---|
| C# / .NET | Programmiersprache |
| WPF | Benutzeroberfläche (MVVM) |
| Entity Framework Core | Datenbankzugriff (ORM) |
| SQLite | Lokale Datenbank |

---

## ✅ Funktionen

### Implementiert
- [x] Workout-Listenansicht – alle Trainingseinheiten auf einen Blick
- [x] Workout anlegen – Formular mit Datum, Sportart, Dauer, Distanz und Notiz
- [x] Sportart-Auswahl per Dropdown (Enum: Schwimmen, Radfahren, Laufen)
- [x] Workout löschen – ausgewähltes Workout wird aus der Datenbank entfernt

### Geplant
- [ ] Workout bearbeiten – schnelle Anpassung eines bestehenden Workouts
- [ ] Workout-Detailansicht (`WorkoutDetailView`) über „Anzeigen"-Button
- [ ] Statistische Auswertungen (z. B. Pace-Berechnung in der Detailansicht)
- [ ] Strava API-Anbindung – automatischer Import von Aktivitäten
- [ ] Filterung der Liste nach Sportart oder Zeitraum
- [ ] Diagramme / Verlaufsansicht der Trainingsdaten

---

## 🗂️ Projektstruktur

```
SwimBikeRun/
├── Data/
│   └── ApplicationDbContext.cs     # Entity Framework DbContext
├── Helpers/
│   └── RelayCommand.cs             # ICommand-Implementierung
├── Models/
│   └── Trainingseinheit.cs         # Datenmodell + SportartTyp Enum
├── ViewModels/
│   ├── MainViewModel.cs            # Navigation zwischen Views
│   ├── WorkoutListeViewModel.cs    # Listenansicht-Logik
│   └── WorkoutAnlegenViewModel.cs  # Formular-Logik
└── Views/
    ├── MainWindow.xaml             # Hauptfenster mit Header & Sidebar
    ├── WorkoutListeView.xaml       # Listenansicht
    └── WorkoutAnlegenView.xaml     # Anlegen-Formular
```

---

## 🚀 Installation & Start

1. Repository klonen
```bash
git clone https://github.com/dein-name/SwimBikeRun.git
```

2. Projekt in Visual Studio öffnen

3. NuGet-Pakete wiederherstellen
```bash
dotnet restore
```

4. Anwendung starten
```
Strg + F5
```

> Die Datenbank wird beim ersten Start automatisch angelegt (`EnsureCreated()`).

---

## 📐 Architektur

Die Anwendung folgt dem **MVVM-Muster** (Model – View – ViewModel):

- **Model** – Datenklassen und Datenbankzugriff (`Trainingseinheit`, `ApplicationDbContext`)
- **ViewModel** – Logik und Commands, kein direkter UI-Zugriff
- **View** – reines XAML, gebunden per `DataBinding` ans ViewModel

Die Navigation zwischen Views erfolgt über `AktuelleView` im `MainViewModel` in Kombination mit `DataTemplates` im `MainWindow`.
