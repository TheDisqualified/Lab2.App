using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Lab2.Model
{
    /// <summary>
    /// Represents a database for managing airport data and operations.
    /// Implements the IDatabase interface.
    /// </summary>
    public class Database : IDatabase
    {
        private const string filename = "airports.db";
        private string airportsFile;
        private ObservableCollection<Airport> airports;
        private JsonSerializerOptions options;

        /*
        private string connString;

        /// <summary>
        /// Constructs an AirportRepository with the specified database connection string.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        public AirportRepository(string connectionString)
        {
            connString = connectionString;
        } */

        /// <summary>
        /// Initializes a new instance of the Database class.
        /// </summary>
        public Database()
        {
            SelectAllAirports();
            options = new JsonSerializerOptions { WriteIndented = true };

            CreateTable(GetConnectinoString()); // Delete after first use
        }

        /// <summary>
        /// Gets the database connection string.
        /// </summary>
        /// <returns>The connection string.</returns>
        static String GetConnectinoString()
        {
            var connStringBuilder = new NpgsqlConnectionStringBuilder();
            connStringBuilder.Host = "stormy-ocelot-12775.5xj.cockroachlabs.cloud";
            connStringBuilder.Port = 26257;
            connStringBuilder.SslMode = SslMode.VerifyFull;
            connStringBuilder.Username = "mprogers";
            connStringBuilder.Password = FetchPassword();
            connStringBuilder.Database = "defaultdb";
            connStringBuilder.ApplicationName = "whatever";
            connStringBuilder.IncludeErrorDetail = true;

            return connStringBuilder.ConnectionString;
        }

        /// <summary>
        /// Fetches the CockroachDB password from configuration settings.
        /// </summary>
        /// <returns>The password or an empty string if not found.</returns>
        static String FetchPassword()
        {
            IConfiguration config = new ConfigurationBuilder().AddUserSecretes<Database>().Build();
            return config["CockroachDBPassword"] ?? "";
        }

        /// <summary>
        /// Creates the "Airports" table in the database if it doesn't exist.
        /// </summary>
        /// <param name="connString">The database connection string.</param>
        static void CreateTable(string connString)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            new NpgsqlCommand("CREATE TABLE IF NOT EXiSTS Airports (id VARCHAR(4) PRIMARY KEY, city VARCHAR(255), rating INT, dateVisisted TIMESTAMP", conn).ExecuteNonQuery();
        }

        /// <summary>
        /// Selects all airports from the database.
        /// </summary>
        /// <returns>An ObservableCollection containing all airports.</returns>
        public ObservableCollection<Airport>? SelectAllAirports()
        {
            airports.Clear();
            var conn = new NpgsqlConnection(connString);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT id, city, rating, dateVisited FROM airports", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                String id = reader.GetString(0);
                String city = reader.GetString(1);
                Int32 rating = reader.GetInt32(2);
                DateTime dateVisited = reader.GetDateTime(3);
                Airport airportToAdd = new(id, city, dateVisited, rating);
                airports.Add(airportToAdd);
                Console.WriteLine(airportToAdd);
            }

            return airports;
        }

        /// <summary>
        /// Selects an airport by its unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the airport.</param>
        /// <returns>The selected airport or null if not found.</returns>
        public Airport SelectAirport(string id)
        {
            foreach (Airport airport in airports)
            {
                if (airport.Id == id)
                {
                    return airport;
                }
            }
            return null;
        }

        /// <summary>
        /// Updates an airport's information in the database.
        /// </summary>
        /// <param name="airportToUpdate">The airport to be updated.</param>
        /// <param name="city">The new city value for the airport.</param>
        /// <param name="rating">The new rating value for the airport.</param>
        /// <param name="dateVisited">The new dateVisited value for the airport.</param>
        /// <returns>True if the update is successful; otherwise, false.</returns>
        public Boolean UpdateAirport(Airport currentAirport, string city, DateTime date, int rating)
        {
            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE airports SET city = @city, rating = @rating, dateVisited = @dateVisited WHERE id = @id;";

                // Set parameters for the SQL command
                cmd.Parameters.AddWithValue("id", currentAirport.Id);
                cmd.Parameters.AddWithValue("city", city);
                cmd.Parameters.AddWithValue("rating", rating);
                cmd.Parameters.AddWithValue("dateVisited", date);

                // Execute the SQL command
                var numAffected = cmd.ExecuteNonQuery();

                // Refresh the list of airports after the update
                SelectAllAirports();
            }
            catch (Npgsql.PostgresException pe)
            {
                Console.WriteLine("Update failed, {0}", pe);
                return false;
            }
            return true;
        }


        /// <summary>
        /// Deletes an airport from the database.
        /// </summary>
        /// <param name="airportToDelete">The airport to be deleted.</param>
        /// <returns>True if the deletion is successful; otherwise, false.</returns>
        public Boolean DeleteAirport(Airport airportToDelete)
        {
            var conn = new NpgsqlConnection(connString);
            conn.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM airports WHERE id = @id";
            cmd.Parameters.AddWithValue("id", airportToDelete.Id);

            int numDeleted = cmd.ExecuteNonQuery();

            // Refresh the list of airports after the deletion
            if (numDeleted > 0)
            {
                SelectAllAirports();
            }
            return numDeleted > 0;
        }

        /// <summary>
        /// Inserts a new airport into the database.
        /// </summary>
        /// <param name="airport">The airport to be inserted.</param>
        /// <returns>True if the insertion is successful; otherwise, false.</returns>
        public Boolean InsertAirport(Airport airport)
        {
            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO airports (id, city, rating, dateVisited) VALUES (@id, @city, @rating, @dateVisited)";

                // Set parameters for the SQL command
                cmd.Parameters.AddWithValue("id", airport.Id);
                cmd.Parameters.AddWithValue("city", airport.City);
                cmd.Parameters.AddWithValue("rating", airport.Rating);
                cmd.Parameters.AddWithValue("dateVisited", airport.DateVisited);

                // Execute the SQL command
                cmd.ExecuteNonQuery();

                // Refresh the list of airports after the insertion
                SelectAllAirports();
            }
            catch (Npgsql.PostgresException pe)
            {
                Console.WriteLine("Insert failed, {0}", pe);
                return false;
            }
            return true;
        }


    }
}