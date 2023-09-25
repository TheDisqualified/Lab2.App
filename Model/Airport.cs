using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Lab2.Model;

public class Airport : INotifyPropertyChanged
{
    String _id;
    String _city;
    int _rating;
    DateTime _dateVisited;
    const int _MIN_RATING = 1;
    const int _MAX_RATING = 5;
    public Airport() { }
    public Airport(string id, string city, DateTime date, int rating)
    {
        Id = id;
        City = city;
        DateVisited = date;
        Rating = rating;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [JsonPropertyName("id")]
    public String Id
    {
        get { return this._id; }
        set 
        {   _id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    [JsonPropertyName("city")]
    public String City
    {
        get { return _city; }
        set 
        {
            _city = value;
            OnPropertyChanged(nameof(City));
        }
    }

    [JsonPropertyName("dateVisited")]
    public DateTime DateVisited
    {
        get { return _dateVisited; }
        set 
        {
            _dateVisited = value;
        }
    }

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
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
