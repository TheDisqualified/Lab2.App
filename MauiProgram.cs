using Lab2.Model;
using Microsoft.Extensions.Logging;

/**
Name: Evan H 
Description: Lab 3
Date: 10/2/2023
Bugs: Occasionally the connection to the database fails without warning. 
Reflection: This assignment was managable, taking IS 315 and building Snowflake SQL Tables helped with 
understanding with understanding what was required for the SQL portion and reviewing the slides made 
the actaul calling of the database fairly straightforward. I was confused on how to connect to the database and GitHub
as I just don't have enough experience with the technology to feel confident with it. I still hate creating buttons
and UI elements - front end work is confusing and boring. 
*/


namespace Lab2;

/// <summary>
/// Static class responsible for configuring and creating a MauiApp instance.
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// Static instance of the IBusinessLogic implementation for managing business logic.
    /// </summary>
    public static IBusinessLogic BusinessLogic = new BusinessLogic();

    /// <summary>
    /// Creates and configures a MauiApp instance for the application.
    /// </summary>
    /// <returns>The configured MauiApp instance.</returns>
    public static MauiApp CreateMauiApp()
    {
        // Create a builder for configuring the MauiApp instance.
        var builder = MauiApp.CreateBuilder();

        // Configure the MauiApp with the following settings:

        // Use the App class as the main application entry point.
        builder.UseMauiApp<App>()

        // Configure custom fonts for the application.
        .ConfigureFonts(fonts =>
        {
            // Add the "OpenSans-Regular.ttf" font with the alias "OpenSansRegular".
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");

            // Add the "OpenSans-Semibold.ttf" font with the alias "OpenSansSemibold".
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

#if DEBUG
        // Enable debug logging in DEBUG builds.
        builder.Logging.AddDebug();
#endif

        // Build and return the configured MauiApp instance.
        return builder.Build();
    }
}

