using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Lab2.Model
{
    /// <summary>
    /// Represents an airport with properties for various details.
    /// Implements INotifyPropertyChanged for data binding.
    /// </summary>
    public class Airport : INotifyPropertyChanged
    {
        private string _id;
        private string _city;
        private int _rating;
        private DateTime _dateVisited;
        private const int _MIN_RATING = 1;
        private const int _MAX_RATING = 5;

        /// <summary>
        /// Default constructor for the Airport class.
        /// </summary>
        public Airport() { }

        /// <summary>
        /// Parameterized constructor for the Airport class.
        /// Initializes airport properties with specified values.
        /// </summary>
        /// <param name="id">The unique identifier of the airport.</param>
        /// <param name="city">The city where the airport is located.</param>
        /// <param name="date">The date when the airport was visited.</param>
        /// <param name="rating">The rating of the airport.</param>
        public Airport(string id, string city, DateTime date, int rating)
        {
            Id = id;
            City = city;
            DateVisited = date;
            Rating = rating;
        }

        /// <summary>
        /// Event handler for property changes, required for data binding.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the unique identifier of the airport.
        /// Triggers property change notification when modified.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id
        {
            get { return this._id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        /// <summary>
        /// Gets or sets the city where the airport is located.
        /// Triggers property change notification when modified.
        /// </summary>
        [JsonPropertyName("city")]
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        /// <summary>
        /// Gets or sets the date when the airport was visited.
        /// </summary>
        [JsonPropertyName("dateVisited")]
        public DateTime DateVisited
        {
            get { return _dateVisited; }
            set
            {
                _dateVisited = value;
            }
        }

        /// <summary>
        /// Gets or sets the rating of the airport.
        /// Triggers property change notification when modified.
        /// Validates that the rating is within the range of 1 to 5.
        /// </summary>
        [JsonPropertyName("rating")]
        public int Rating
        {
            get { return this._rating; }
            set
            {
                // Validate that the rating is within the range of 1 to 5.
                if (value >= _MIN_RATING && value <= _MAX_RATING)
                {
                    _rating = value;
                    OnPropertyChanged(nameof(Rating));
                }
            }
        }

        /// <summary>
        /// Invokes the PropertyChanged event to notify property changes.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Returns a formatted string representation of the airport.
        /// </summary>
        /// <returns>A string containing airport details.</returns>
        public override string ToString()
        {
            return string.Format("Airport Id: {0}, City: {1}, Date Visited: {2}, Rating: {3}", Id, City, DateVisited, Rating);
        }
    }
}