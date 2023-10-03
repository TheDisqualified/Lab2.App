using Lab2.Model;
using Microsoft.Extensions.Logging;

/**
Name: Evan H 
Description: Lab 3
Date: 10/2/2023
Bugs: Occasionally the connection to the database fails without warning. 
Reflection: This assignment was another struggle, taking IS 315 and building Snowflake SQL Tables helped with 
understanding with understanding what was required for the SQL portion and reviewing the slides made 
the actaul calling of the database fairly straightforward. I was confused on how to connect to the database and GitHub
as I just don't have enough experience with the technology to feel confident with it. I still hate creating buttons
and UI elements - front end work is confusing and boring. All of these elements of the project were manageable, except for my
connection string bug. I was able to successfully connect to the database when I left office hours, but after running the code twice,
I repeatedly received the same error. I double checked my code and the canvas example and it was exactly as it should. I then double
checked my host name, username, and password on cockroach, but that was accurate. Then, I tried resetting my password, which didn't work.
Next, I tried putting the host name into google, as if it were a URL, because maybe that would work like a URL or IP address, 
but that yielded a DNS error. After that I tried connecting to Jordyn's database, which also failed. Finally, I tried to virtual machine into the
lab because I was not about to drive 30 minutes to get into the computer for a shot at maybe fixing the issue, but I was not able to get Android Studio
on the machine, so I am at a loss for worlds. I have tried everything in my power to resolve the issue. I consultanted old stack overflow
and chatGPT, but the information I found was not relevant to my issue. If I was in the professional world and could not consultant with
a more senior team member, I would try setting this up on a different machine, but I honestly think that Cockroach DB is doing something differently
in the background and I would call or email my first point of contact in our service agreement. Attached are screenshots ofmy issue. Sorry, that this has 
happened, but I legitatemely gave my best effort and thought I did fairly well up until this point in the lab. 
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

