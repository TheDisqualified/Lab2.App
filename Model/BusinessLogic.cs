using System.Collections.ObjectModel;

namespace Lab2.Model
{
    /// <summary>
    /// Implementation of the IBusinessLogic interface for managing airport data and operations.
    /// </summary>
    public class BusinessLogic : IBusinessLogic
    {
        private IDatabase Database { get; set; }

        /// <summary>
        /// Gets an ObservableCollection of airports, bound to the database's SelectAllAirports method.
        /// </summary>
        public ObservableCollection<Airport> Airports
        {
            get { return Database.SelectAllAirports(); }
        }

        /// <summary>
        /// Default constructor initializes the database.
        /// </summary>
        public BusinessLogic()
        {
            Database = new Database();

        }

        /// <summary>
        /// Adds a new airport to the database.
        /// </summary>
        /// <param name="id">The unique identifier of the airport.</param>
        /// <param name="city">The city where the airport is located.</param>
        /// <param name="dateVisited">The date when the airport was visited.</param>
        /// <param name="rating">The rating of the airport.</param>
        /// <returns>An AirportAdditionError indicating the result of the addition.</returns>
        public AirportAdditionError AddAirport(string id, string city, string dateVisited, string rating)
        {
            if (city.Length > 0 && city.Length < 26)
            {
                if (int.TryParse(rating, out int ratingValue) && ratingValue > 0 && ratingValue < 6)
                {
                    if (DateTime.TryParse(dateVisited, out DateTime dateVisitedValue) &&
                        dateVisitedValue <= DateTime.Today && dateVisitedValue >= new DateTime(1907, 12, 17))
                    {
                        if (id.Length > 2 && id.Length < 5)
                        {
                            if (DuplicateAirportAbsent(id))
                            {
                                if (Database.InsertAirport(new Airport(id, city, dateVisitedValue, ratingValue)))
                                {
                                    return AirportAdditionError.NoError;
                                }
                                return AirportAdditionError.DBAdditionError;
                            }
                            return AirportAdditionError.ExistingAirportError;
                        }
                        return AirportAdditionError.InvalidIdLength;
                    }
                    return AirportAdditionError.InvalidDate;
                }
                return AirportAdditionError.InvalidNumStars;
            }
            return AirportAdditionError.InvalidCityLength;
        }

        /// <summary>
        /// Checks if an airport with the given ID already exists in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the airport to check.</param>
        /// <returns>True if the airport with the given ID does not exist; otherwise, false.</returns>
        private bool DuplicateAirportAbsent(String id)
        {
            ObservableCollection<Airport> airports = Database.SelectAllAirports();
            foreach (Airport existingAirport in airports)
            {
                if (existingAirport.Id.Equals(id))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Deletes an airport from the database by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the airport to delete.</param>
        /// <returns>An AirportDeletionError indicating the result of the deletion.</returns>
        public AirportDeletionError DeleteAirport(string id)
        {
            Airport airportToGo = Database.SelectAirport(id);
            if (airportToGo != null)
            {
                if (Database.DeleteAirport(airportToGo))
                {
                    return AirportDeletionError.NoError;
                }
                else
                {
                    return AirportDeletionError.DBDeletionError;
                }
            }
            return AirportDeletionError.AirportNotFound;
        }

        /// <summary>
        /// Edits an existing airport's details.
        /// </summary>
        /// <param name="currentAirport">The airport to edit.</param>
        /// <param name="city">The new city value.</param>
        /// <param name="dateVisited">The new dateVisited value.</param>
        /// <param name="rating">The new rating value.</param>
        /// <returns>An AirportEditError indicating the result of the edit operation.</returns>
        public AirportEditError EditAirport(Airport currentAirport, string city, string dateVisited, string rating)
        {
            if (Database.UpdateAirport(currentAirport, city, DateTime.Parse(dateVisited), int.Parse(rating)))
            {
                return AirportEditError.NoError;
            }
            return AirportEditError.DBEditError;
        }

        /// <summary>
        /// Calculates and returns statistics based on the number of airports visited by the user.
        /// </summary>
        /// <returns>A string containing the user's statistics.</returns>
        public string CalculateStatistics()
        {
            String statsOutput = ""; // Initialize an empty string to store statistics output.
            ObservableCollection<Airport> airports = Database.SelectAllAirports(); // Get the list of all airports.
            int airportCount = airports.Count; // Count the number of visited airports.

            // Determine the user's badge status based on the number of visited airports.
            if (airportCount < 42)
            {
                statsOutput = airportCount + " airports visited; " + (42 - airportCount) + " airports remaining until Bronze";
            }
            else if (airportCount == 42)
            {
                statsOutput = airportCount + " airports visited; Congrats! You earned Bronze";
            }
            else if (airportCount < 84)
            {
                statsOutput = airportCount + " airports visited; " + (84 - airportCount) + " airports remaining until Silver";
            }
            else if (airportCount == 84)
            {
                statsOutput = airportCount + " airports visited; Congrats! You earned Silver";
            }
            else if (airportCount < 125)
            {
                statsOutput = airportCount + " airports visited; " + (125 - airportCount) + " airports remaining until Gold";
            }
            else if (airportCount == 125)
            {
                statsOutput = airportCount + " airports visited; Congrats! You earned Gold";
            }
            else
            {
                statsOutput = airportCount + " airports visited; You have no more badges to earn.";
            }

            // Return the calculated statistics.
            return statsOutput;
        }
    }
}