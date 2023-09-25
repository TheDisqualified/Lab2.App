using System.Collections.ObjectModel;

namespace Lab2.Model;

public interface IBusinessLogic
{
    public ObservableCollection<Airport> Airports { get;}

    public AirportAdditionError AddAirport(String id, String city, DateTime dateVisited, int rating);
    public AirportDeletionError DeleteAirport(String id);
    public AirportEditError EditAirport(Airport currentAirport, String city, DateTime dateVisited, int rating);
}
