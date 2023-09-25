using System.Collections.ObjectModel;

namespace Lab2.Model;

public interface IDatabase
{
    public ObservableCollection<Airport> SelectAllAirports();
    public Airport SelectAirport(string id);
    public AirportAdditionError InsertAirport(Airport airport);
    public AirportDeletionError DeleteAirport(Airport airportToDelete);
    public AirportEditError UpdateAirport(Airport currentAirport, String city, DateTime date, int rating);
}
