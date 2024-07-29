TouristGuide & LocationsApi
Overview

TouristGuide is an Android application developed with Xamarin Android. It allows users to explore tourist attractions in Greece by displaying them on a map with markers. Users can click on a marker to view an image, read a description, and play a sound recording describing the sight. Additionally, if a user's current location is close to the sight, the sound recording will play automatically.

LocationsApi is a web application that enables administrators to create and manage tourist attraction locations. Administrators can provide information such as the name, area, postal code, description, image, and sound recording for each location. They can also set the distance at which the sound recording will play automatically for the Android application users. The Android application fetches data from this API to present it to the user.
Table of Contents

    Getting Started
    TouristGuide Android Application
    LocationsApi Web Application
    Running the Solution
    Configuration
    Usage Instructions

Getting Started
Prerequisites

    Visual Studio with Xamarin installed
    .NET Core SDK
    Android Emulator or Android device
    Web hosting service for LocationsApi

Installation

    Clone the repository:

    bash

    git clone https://github.com/your-username/your-repo.git
    cd your-repo

TouristGuide Android Application
Features

    Display tourist attractions on a map with markers.
    Click on a marker to view an image, read a description, and play a sound recording.
    Automatically play the sound recording when the user is close to the sight.

Directory Structure

markdown

/TouristGuide
    /Resources
    /Assets
    /MainActivity.cs
    /MapActivity.cs
    ...

Dependencies

    Xamarin.Forms
    Google Maps API
    Xamarin.Essentials

Building and Running

    Open the TouristGuide project in Visual Studio.
    Configure the API endpoint URL (see Configuration).
    Build and run the application on an emulator or Android device.

LocationsApi Web Application
Features

    Create and manage tourist attractions.
    Provide name, area, postal code, description, image, and sound recording for each location.
    Set the distance for automatic sound playback.

Directory Structure

bash

/LocationsApi
    /Controllers
    /Models
    /Views
    /wwwroot
    ...

Dependencies

    ASP.NET Core
    Entity Framework Core

Building and Running

    Open the LocationsApi project in Visual Studio.
    Configure the database connection string in appsettings.json.
    Run the following command to create the database schema:

    bash

    dotnet ef database update

    Build and run the web application.

Running the Solution
Hosting LocationsApi

    Host the LocationsApi web application on a web server or cloud service.
    Ensure the API is accessible via a public URL.

Configuring TouristGuide

    In the TouristGuide project, update the API endpoint URL using Xamarin.Essentials.Preferences:

csharp

Xamarin.Essentials.Preferences.Set("ApiEndpoint", "https://your-locationsapi-url.com");

Running TouristGuide

    Open the TouristGuide project in Visual Studio.
    Run the application on an Android emulator or device.

Configuration
LocationsApi Configuration

    appsettings.json: Update the database connection string and other settings as needed.

json

{
  "ConnectionStrings": {
    "DefaultConnection": "YourConnectionStringHere"
  },
  ...
}

TouristGuide Configuration

    Set the API endpoint URL using Xamarin.Essentials.Preferences in the code:

csharp

public static class Configuration
{
    public static void Initialize()
    {
        Xamarin.Essentials.Preferences.Set("ApiEndpoint", "https://your-locationsapi-url.com");
    }
}

Usage Instructions
Administrator Instructions

    Login to the LocationsApi web application with your administrator account.
    Click on "Locations" in the main menu to view a list of existing locations.
    Press "Create New" to open the form for creating a new location.
    Fill out the form with the location's name, area, postal code, description, image, and sound recording. Set the "Meters to Notify" attribute.
    Submit the form to create the new location.

Test User Instructions

    Launch the TouristGuide Android application.
    Press "Map" on the main page.
    Navigate the map to the location you previously set in the LocationsApi.
    The location is indicated with a Greek flag icon.
    Press on the icon to open a card showing the description and image of the location.
    Press the play button to hear the audio track set in LocationsApi.
    If you are close enough to the location, the audio track will play automatically.

Contributing

    Fork the repository.
    Create a new branch (git checkout -b feature-branch).
    Make your changes.
    Commit your changes (git commit -am 'Add new feature').
    Push to the branch (git push origin feature-branch).
    Create a new Pull Request.

License

This project is licensed under the MIT License - see the LICENSE file for details.
