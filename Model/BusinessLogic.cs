using System.Collections.ObjectModel;

namespace Lab2.Model;

public class BusinessLogic : IBusinessLogic
{
    private IDatabase Database { get; set; }

    public ObservableCollection<Airport> Airports
{
        get { return Database.SelectAllAirports(); }
    }

    public BusinessLogic()
    {
        Database = new Database();
    }

    // Adds a movie to the database
    // TODO: Check if the movie already exists (how?)
    public AirportAdditionError AddAirport(String id, String city, DateTime dateVisited, int rating)
    {
        if (city.Length > 0 && rating > 0) // see, brains - it can check
        {
            Database.InsertAirport(new Airport(id, city, dateVisited, rating)); // is this foolproof? what could go wrong?
            return AirportAdditionError.NoError;
        }
        return AirportAdditionError.InvalidIdLength;  // TODO: fix depending 
    }

    // Deletes a movie
    // Also note that DeleteMovie() doesn't return anything - how will the UI layer know if it should DisplayAlert()?
    public AirportDeletionError DeleteAirport(String id)
    {
        Airport airportToGo = Database.SelectAirport(id);
        if (airportToGo != null)
        {
            return Database.DeleteAirport(airportToGo);
        }
        else
        {
            return AirportDeletionError.AirportNotFound;
        }
    }

    // Edits a movie - notice that imdbId cannot be changed, ever
    public AirportEditError EditAirport(Airport currentAirport, String city, DateTime dateVisited, int rating)
    {
            // Now that you have successfully parsed dateVisited and rating, you can use them in your UpdateAirport method.
            return Database.UpdateAirport(currentAirport, city, dateVisited, rating);
    }
}
