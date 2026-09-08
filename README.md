```markdown
# 🏊 🚴 🏃 SwimBikeRun

Eine WPF-Desktopanwendung zur Verwaltung und Auswertung von Triathlon-Trainingseinheiten – gebaut mit C#, WPF und Entity Framework Core.

---

## 📋 Über das Projekt

SwimBikeRun ist ein persönliches Trainingstagebuch für Triathleten. Ziel ist es, Trainingseinheiten strukturiert zu erfassen, auszuwerten und langfristig die sportliche Leistungsfähigkeit zu optimieren. Die Anwendung folgt dem MVVM-Architekturmuster, nutzt eine lokale SQLite-Datenbank zur persistenten Speicherung und importiert Workouts automatisch über die Intervals.icu API.

---

## 🛠️ Technologien

| Technologie | Verwendung |
|---|---|
| C# / .NET | Programmiersprache |
| WPF | Benutzeroberfläche (MVVM-Pattern) |
| Entity Framework Core | Datenbankzugriff (ORM) |
| SQLite | Lokale Datenbank |
| LiveChartsCore (SkiaSharp) | Diagramme & Verlaufsansicht |
| Intervals.icu REST API | Workout-Import via HTTP GET |
| xUnit | Unit Tests |
| Dependency Injection | Service-Registrierung via Microsoft.Extensions.DI |

---

## ✅ Funktionen

### Implementiert
- [x] **Dashboard** – Wochenvolumen pro Sportart, Fortschrittsbalken, Deload-Erkennung, Verlaufsdiagramm (8 Wochen)
- [x] **Workout-Listenansicht** – alle Trainingseinheiten auf einen Blick
- [x] **Workout anlegen** – Formular mit Datum, Sportart, Dauer, Distanz und Notiz
- [x] **Workout bearbeiten** – bestehende Trainingseinheit anpassen
- [x] **Workout löschen** – ausgewähltes Workout aus der Datenbank entfernen
- [x] **Workout-Detailansicht** – Detailinfos inkl. Pace-Berechnung
- [x] **Sportart-Auswahl per Dropdown** – Enum: Schwimmen, Radfahren, Laufen
- [x] **Pace-Berechnung** – sportartspezifisch via `PaceService` (min/km, min/100m, km/h)
- [x] **Intervals.icu API Import** – Workouts per Button aus Intervals.icu importieren
- [x] **Unit Tests** – PaceService mit xUnit getestet inkl. Edge Cases

### Geplant
- [ ] Duplikate beim Import vermeiden
- [ ] Datum-Parameter für Import konfigurierbar machen
- [ ] Farbe der Fortschrittsbalken bei erreichtem Wochenziel ändern
- [ ] Filterung der Liste nach Sportart oder Zeitraum
- [ ] Deload-Button – nächste Deload-Woche berechnen und anzeigen
- [ ] Wettkampf-Countdown (neue `Wettkampf` Klasse)
- [ ] Background Service für automatischen Import
- [ ] KI-Empfehlungen basierend auf Trainingsdaten

---

## ⚙️ Intervals.icu API Setup

Die App importiert Workouts über die Intervals.icu API. Credentials werden **nicht** im Code gespeichert sondern über `secrets.json` verwaltet.

1. `appsettings.json` liegt als Vorlage im Projekt:
```json
{
  "Intervals": {
    "AthleteId": "",
    "ApiKey": ""
  }
}
```

2. Eigene Credentials per Visual Studio eintragen:
```
Rechtsklick auf Projekt → Manage User Secrets
```

```json
{
  "Intervals": {
    "AthleteId": "deine_athlete_id",
    "ApiKey": "dein_api_key"
  }
}
```

> API Key unter intervals.icu → Einstellungen → API Key generieren.

---

## 🗂️ Projektstruktur

```
SwimBikeRun/
├── Converters/
│   └── IntervalsConverter.cs           # API Daten → Trainingseinheit
├── Data/
│   └── ApplicationDbContext.cs         # Entity Framework DbContext
├── Helpers/
│   └── RelayCommand.cs                 # ICommand-Implementierung
├── Models/
│   ├── Trainingseinheit.cs             # Datenmodell + SportartTyp Enum
│   └── IntervalsActivity.cs            # Rohe API Daten von Intervals.icu
├── Services/
│   ├── PaceService.cs                  # Sportartspezifische Pace-Berechnung
│   ├── IntervalsService.cs             # HTTP Request zur Intervals.icu API
│   └── ImportService.cs                # Koordiniert den gesamten Import
├── ViewModels/
│   ├── MainViewModel.cs                # Navigation & Commands
│   ├── DashboardViewModel.cs           # Dashboard-Logik & LiveCharts
│   ├── WorkoutListeViewModel.cs        # Listenansicht
│   ├── WorkoutAnlegenViewModel.cs      # Workout anlegen
│   ├── WorkoutBearbeitenViewModel.cs   # Workout bearbeiten
│   └── WorkoutDetailViewModel.cs       # Detailansicht
└── Views/
    ├── MainWindow.xaml                 # Hauptfenster mit Header & Sidebar
    ├── DashboardView.xaml              # Dashboard
    ├── WorkoutListeView.xaml           # Listenansicht
    ├── WorkoutAnlegenView.xaml         # Anlegen-Formular
    ├── WorkoutBearbeitenView.xaml      # Bearbeiten-Formular
    └── WorkoutDetailView.xaml          # Detailansicht

SwimBikeRun.Tests/
└── PaceServiceTests.cs                 # xUnit Tests für PaceService
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

4. Intervals.icu Credentials in `secrets.json` eintragen (siehe oben)

5. Anwendung starten
```
Strg + F5
```

> Die SQLite-Datenbank wird beim ersten Start automatisch angelegt (`EnsureCreated()`).

---

## 📐 Architektur

Die Anwendung folgt dem **MVVM-Muster** (Model – View – ViewModel):

- **Model** – Datenklassen (`Trainingseinheit`, `IntervalsActivity`, `SportartTyp`)
- **Service** – Geschäftslogik außerhalb der ViewModels (`PaceService`, `IntervalsService`, `ImportService`)
- **Converter** – Übersetzung zwischen API-Format und internem Modell (`IntervalsConverter`)
- **ViewModel** – Logik und Commands, kein direkter UI-Zugriff
- **View** – reines XAML, gebunden per DataBinding ans ViewModel

Die Navigation erfolgt über `AktuelleView` im `MainViewModel` kombiniert mit `DataTemplates` – WPF wählt automatisch die passende View anhand des ViewModel-Typs.

---

## 💡 Prinzipien

- **SOLID** – Single Responsibility (z.B. `PaceService` nur für Berechnungen, `ImportService` nur für Koordination)
- **Dependency Injection** – Services und DbContext per DI durchgereicht
- **Clean Code** – sprechende Namen, keine Logik im Code-Behind
- **Credentials-Sicherheit** – API Keys nur lokal via `secrets.json`, nie im Repository
```