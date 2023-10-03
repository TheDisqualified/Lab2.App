using System.Collections.ObjectModel;

namespace Lab2.Model
{
    /// <summary>
    /// Represents the interface for a database containing airport data.
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// Retrieves a collection of all airports from the database.
        /// </summary>
        /// <returns>
        /// An <see cref="ObservableCollection{T}"/> of <see cref="Airport"/> objects or <c>null</c> if there are no airports.
        /// </returns>
        ObservableCollection<Airport>? SelectAllAirports();

        /// <summary>
        /// Retrieves an airport based on its unique identifier from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the airport to retrieve.</param>
        /// <returns>
        /// The <see cref="Airport"/> object associated with the given ID, or <c>null</c> if not found.
        /// </returns>
        Airport SelectAirport(string id);

        /// <summary>
        /// Inserts a new airport into the database.
        /// </summary>
        /// <param name="airport">The <see cref="Airport"/> object to be inserted.</param>
        /// <returns><c>true</c> if the insertion is successful, <c>false</c> otherwise.</returns>
        bool InsertAirport(Airport airport);

        /// <summary>
        /// Deletes an airport from the database.
        /// </summary>
        /// <param name="airportToDelete">The <see cref="Airport"/> object to be deleted.</param>
        /// <returns><c>true</c> if the deletion is successful, <c>false</c> otherwise.</returns>
        bool DeleteAirport(Airport airportToDelete);

        /// <summary>
        /// Updates airport information, such as city, date, and rating, in the database.
        /// </summary>
        /// <param name="currentAirport">The <see cref="Airport"/> object to be updated.</param>
        /// <param name="city">The new city value for the airport.</param>
        /// <param name="date">The new date value for the airport.</param>
        /// <param name="rating">The new rating value for the airport.</param>
        /// <returns><c>true</c> if the update is successful, <c>false</c> otherwise.</returns>
        bool UpdateAirport(Airport currentAirport, string city, DateTime date, int rating);
    }
}



