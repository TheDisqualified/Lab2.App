using System.Collections.ObjectModel;

namespace Lab2.Model
{
    /// <summary>
    /// Represents the interface for a database containing airport data.
    /// </summary>
    public interface IDatabase
    {
        ObservableCollection<Airport>? SelectAllAirports();
        Airport SelectAirport(string id);
        Boolean InsertAirport(Airport airport);
        Boolean DeleteAirport(Airport airportToDelete);
        Boolean UpdateAirport(Airport currentAirport, string city, DateTime date, int rating);
    }
}

