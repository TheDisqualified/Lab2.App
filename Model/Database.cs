using System.Collections.ObjectModel;
using System.Text.Json;

namespace Lab2.Model;

public class Database : IDatabase
{
    const String filename = "airports.db";
    String airportsFile;
    ObservableCollection<Airport> airports;
    JsonSerializerOptions options;

    public Database()
    {
        SelectAllAirports();
        options = new JsonSerializerOptions { WriteIndented = true };
    }

    public ObservableCollection<Airport>? SelectAllAirports()
    {
        string jsonString;

        String mainDir = FileSystem.Current.AppDataDirectory;
        airportsFile = String.Format("{0}/{1}", mainDir, filename);
        if (!File.Exists(airportsFile))
        {
            File.CreateText(airportsFile);
            airports = new ObservableCollection<Airport>();
            jsonString = JsonSerializer.Serialize(airports, options);
            File.WriteAllText(airportsFile, jsonString);
            return airports;
        }
        jsonString = File.ReadAllText(airportsFile);
        if(jsonString.Length > 0)
        {
            airports = JsonSerializer.Deserialize<ObservableCollection<Airport>>(jsonString);
        }
        return airports;
    }

    public Airport SelectAirport(String id)
    {
        // Iterate through airports and print their IDs to find the desired airport.
        foreach (Airport airport in airports)
        {
            if (airport.Id == id)
            {
                return airport;
            }
        }
        return null;
    }

    public AirportAdditionError InsertAirport(Airport airport)
    {
        // Check if an airport with the same ID already exists.
        try
        {
            airports.Add(airport);

            string jsonString = JsonSerializer.Serialize(airports, options);
            File.WriteAllText(airportsFile, jsonString);
            return AirportAdditionError.NoError;
        }catch(IOException ioe)
        {
            Console.WriteLine("Error while adding airport: {0}", ioe);
            return AirportAdditionError.DBAdditionError;
        }
    }

    public AirportDeletionError DeleteAirport(Airport airportToDelete)
    {
        try
        {
            if (airports.Contains(airportToDelete))
            {
                var result = airports.Remove(airportToDelete);
                string jsonString = JsonSerializer.Serialize(airports, options);
                File.WriteAllText(airportsFile, jsonString);
                return AirportDeletionError.NoError;
            }
            else
            {
                return AirportDeletionError.AirportNotFound;
            }
        }
        catch(IOException ioe)
        {
            Console.WriteLine("Error while deleting aiport: {0}", ioe);
            return AirportDeletionError.DBDeletionError;
        }
    }

    public AirportEditError UpdateAirport(Airport currentAirport, String city, DateTime date, int rating)
    {
        foreach(Airport airport in airports)
        {
            if(airport.Id == currentAirport.Id)
            {
                airport.City = city;
                airport.DateVisited = date;
                airport.Rating = rating;

                try
                {
                    string jsonString = JsonSerializer.Serialize(airports, options);
                    File.WriteAllText(airportsFile, jsonString);
                    return AirportEditError.NoError;
                }
                catch(IOException ioe)
                {
                    Console.WriteLine("Error while replacing airport: {0}", ioe);
                    return AirportEditError.DBEditError;
                }
            }
        }
        return AirportEditError.NoError;
    }
}
