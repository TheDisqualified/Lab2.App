using System.Collections.ObjectModel;

namespace Lab2.Model
{
    /// <summary>
    /// Represents the interface for business logic related to airports.
    /// </summary>
    public interface IBusinessLogic
    {
        /// <summary>
        /// Gets an ObservableCollection of airports.
        /// </summary>
        ObservableCollection<Airport> Airports { get; }

        /// <summary>
        /// Adds a new airport to the collection.
        /// </summary>
        /// <param name="id">The unique identifier of the airport.</param>
        /// <param name="city">The city where the airport is located.</param>
        /// <param name="dateVisited">The date the airport was visited.</param>
        /// <param name="rating">The rating of the airport.</param>
        /// <returns>An AirportAdditionError indicating the result of the addition.</returns>
        public AirportAdditionError AddAirport(string id, string city, string dateVisited, string rating);

        /// <summary>
        /// Deletes an airport from the collection.
        /// </summary>
        /// <param name="id">The unique identifier of the airport to delete.</param>
        /// <returns>An AirportDeletionError indicating the result of the deletion.</returns>
        public AirportDeletionError DeleteAirport(string id);

        /// <summary>
        /// Edits the details of an existing airport.
        /// </summary>
        /// <param name="currentAirport">The airport to edit.</param>
        /// <param name="city">The new city value.</param>
        /// <param name="dateVisited">The new date value.</param>
        /// <param name="rating">The new rating value.</param>
        /// <returns>An AirportEditError indicating the result of the edit operation.</returns>
        public AirportEditError EditAirport(Airport currentAirport, string city, string dateVisited, string rating);

        /// <summary>
        /// Calculate the number of airports visited and then construct a statement conveying that information..
        /// </summary>
        /// <returns>A string that states how badges the user has and what is needed to get to the next badge.</returns>
        string CalculateStatistics();
    }
}
