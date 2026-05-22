using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.Data.Sqlite;
using SwimBikeRun.Models;
using SwimBikeRun.Data;

namespace SwimBikeRun.Data
{
    public class Datenbankzugriff
    {
        private string _connectionString;

        public Datenbankzugriff(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void DbInitialisieren()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    CREATE TABLE IF NOT EXISTS Trainingseinheiten (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Datum TEXT NOT NULL,
                        Name TEXT NOT NULL,
                        DauerMinuten INTEGER NOT NULL,
                        DistanzKm REAL NOT NULL,
                        Notiz TEXT
                    );
                ";
                command.ExecuteNonQuery();
            }
        }

        // Weitere Methoden zum Einfügen, Abrufen, Aktualisieren und Löschen von Trainingseinheiten können hier hinzugefügt werden
        // Methode zum Hinzufügen einer Trainingseinheit
        public void Add(Trainingseinheit trainingseinheit)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    INSERT INTO Trainingseinheiten (Datum, Name, DauerMinuten, DistanzKm, Notiz)
                    VALUES ($Datum, $Name, $DauerMinuten, $DistanzKm, $Notiz);
                ";
                command.Parameters.AddWithValue("$Datum", trainingseinheit.Datum.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("$Name", trainingseinheit.Name);
                command.Parameters.AddWithValue("$DauerMinuten", trainingseinheit.DauerMinuten);
                command.Parameters.AddWithValue("$DistanzKm", trainingseinheit.DistanzKm);
                command.Parameters.AddWithValue("$Notiz", trainingseinheit.Notiz ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }

        // Neue Methode zum Abrufen aller Trainingseinheiten
        public List<Trainingseinheit> GetAlleTrainingsheiten()
        {
            var trainingseinheiten = new List<Trainingseinheit>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT Id, Datum, Name, DauerMinuten, DistanzKm, Notiz
                    FROM Trainingseinheiten;
                ";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var trainingseinheit = new Trainingseinheit
                        {
                            Id = reader.GetInt32(0),
                            Datum = DateTime.Parse(reader.GetString(1)),
                            Name = reader.GetString(2),
                            DauerMinuten = reader.GetInt32(3),
                            DistanzKm = reader.GetDouble(4),
                            Notiz = reader.IsDBNull(5) ? null : reader.GetString(5)
                        };
                        trainingseinheiten.Add(trainingseinheit);
                    }
                }
            }
            return trainingseinheiten;
        }

    }
}
