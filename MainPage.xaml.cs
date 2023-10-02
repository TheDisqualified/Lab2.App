using Lab2.Model;

namespace Lab2
{
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the MainPage class.
        /// Sets the binding context to the BusinessLogic class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            BindingContext = MauiProgram.BusinessLogic;
        }

        /// <summary>
        /// Handles the event when the "Add Airport" button is clicked.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        void AddAirport_Clicked(System.Object sender, System.EventArgs e)
        {
            // Empty the calculate statistics label
            StatisticsLabel.Text = "";

            // Attempt to add an airport and store the result
            AirportAdditionError result = MauiProgram.BusinessLogic.AddAirport(IdENT.Text, CityENT.Text, DateVisitedENT.Text, RatingENT.Text);

            // Check if there was an error in adding the airport
            if (result != AirportAdditionError.NoError)
            {
                DisplayAlert("Error", result.ToString(), "OK");
            }

            // Refresh the CollectionView after adding the airport
            CV.ItemsSource = null;
            CV.ItemsSource = ((BusinessLogic)BindingContext).Airports;
        }

        /// <summary>
        /// Handles the event when the "Delete Airport" button is clicked.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        void DeleteAirport_Clicked(System.Object sender, System.EventArgs e)
        {
            // Empty the calculate statistics label
            StatisticsLabel.Text = "";

            // Get the currently selected airport
            Airport currentAirport = CV.SelectedItem as Airport;

            // Check if an airport is selected
            if (currentAirport != null)
            {
                // Attempt to delete the selected airport and store the result
                AirportDeletionError result = MauiProgram.BusinessLogic.DeleteAirport(currentAirport.Id);

                // Check if there was an error in deleting the airport
                if (result != AirportDeletionError.NoError)
                {
                    DisplayAlert("Error", result.ToString(), "OK");
                }
            }
            else
            {
                // Handle the case where no airport is selected
                DisplayAlert("Error", "No airport selected", "OK");
            }

            // Refresh the CollectionView after deleting the airport
            CV.ItemsSource = null;
            CV.ItemsSource = ((BusinessLogic)BindingContext).Airports;
        }

        /// <summary>
        /// Handles the event when the "Edit Airport" button is clicked.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        void EditAirport_Clicked(System.Object sender, System.EventArgs e)
        {
            // Empty the calculate statistics label
            StatisticsLabel.Text = "";

            // Get the currently selected airport
            Airport currentAirport = CV.SelectedItem as Airport;

            // Check if an airport is selected
            if (currentAirport != null)
            {
                // Attempt to edit the selected airport and store the result
                AirportEditError result = MauiProgram.BusinessLogic.EditAirport(currentAirport, CityENT.Text, DateVisitedENT.Text, RatingENT.Text);

                // Check if there was an error in editing the airport
                if (result != AirportEditError.NoError)
                {
                    DisplayAlert("Error", result.ToString(), "OK");
                }
            }
            else
            {
                // Handle the case where no airport is selected
                DisplayAlert("Error", "No airport selected", "OK");
            }

            // Refresh the CollectionView after editing the airport
            CV.ItemsSource = null;
            CV.ItemsSource = ((BusinessLogic)BindingContext).Airports;
        }

        /// <summary>
        /// Event handler for the "Calculate Statistics" button click event.
        /// Calculates statistics and updates the StatisticsLabel with the result.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        void CalculateStatistics_Clicked(System.Object sender, System.EventArgs e)
        {
            String statsOutput = MauiProgram.BusinessLogic.CalculateStatistics();

            // Set the Text property of the StatisticsLabel to display the statistics.
            StatisticsLabel.Text = statsOutput;
        }
    }

}
