<h1 style="color:red;">TouristGuide & LocationsApi Overview</h1>

TouristGuide is an Android application developed with Xamarin Android. It allows users to explore tourist attractions in Greece by displaying them on a map with markers. Users can click on a marker to view an image, read a description, and play a sound recording describing the sight. Additionally, if a user's current location is close to the sight, the sound recording will play automatically.


LocationsMarker Api is a web application that enables administrators to create and manage tourist attraction locations that will be used by TouristGuide app to present the sights to the users. Administrators can provide information such as the name, area, postal code, description, image, and sound recording for each location. They can also set the distance at which the sound recording will play automatically for the Android application users. 



## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022 or later. Packages that must be installed:
  -  ASP.NET and web development
  -  Android SDK setup
  -  Xamarin
  - .NET Multi-platform App UI Development
  - .NET desktop development
  - [Hyper-V](https://learn.microsoft.com/en-us/dotnet/maui/android/emulator/hardware-acceleration?view=net-maui-8.0) hardware acceleration 


## Features of TouristGuide Android App:
- Explore Tourist Attractions in Greece:
  -  Displays tourist attractions on a map with interactive markers.
  -  Users can click on a marker to view more details about the location including an image as well as text & audio description. 

- Automatic Audio Playback Based on current Location:
  -  The app tracks the user’s current location and plays the sound recording automatically when the user is near a specific attraction.

## Features of LocationsMarker API (Admin Web Application):

- Manage Tourist Attractions:
  -  Administrators can add, edit, or delete tourist attraction locations for the app.
  -  Manage details like the name, area, postal code, and description of each tourist attraction.
  -  Upload images and sound recordings to provide a richer user experience.

- Proximity-based Sound Triggering:
  -  Set the distance at which the sound recording will play automatically for users near a specific tourist attraction.

## Planned Enhancements (optional):
- Offline Mode: Allow users to explore attractions without internet access.
- User Reviews & Ratings: Enable users to rate and review tourist attractions.
- Personalized Recommendations: Provide recommendations based on user preferences and previous visits.

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/DimitrisPOL/TouristLocationsMarker.git
cd TouristLocationsMarker

```

## Setting Up the Project
1. Install Required Packages

2. Ensure all necessary packages are installed:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

3. Configure the Database Context


```bash
PM> Enable-Migrations

PM> Update-Database

```
4. Set database connection string and prefered map starting coordinates on Location.UI appsettings.developments.json

```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=********;Database=Beacon;Trusted_Connection=True;MultipleActiveResultSets=true"
},
"MapCenter": {
  "Lat": 38.034233,
  "Lng": 23.7068017
}

```
5. Navigate at Locations.UI\Pages\Shared\_Layout.cshtml and fill in a valid Google Map Api Key. Visit this [Page](https://developers.google.com/maps/documentation/javascript/get-api-key) for more informtaion on how to obtain a key.

```bash

<script src="https://maps.googleapis.com/maps/api/js?key=*******************&callback=initMap&libraries=&v=weekly"></script>

```

6. Enable hardware acceleration in your pc. See [here](https://learn.microsoft.com/en-us/dotnet/maui/android/emulator/hardware-acceleration?view=net-maui-8.0) for more information.

7. In the Android applicaton navigate to the constructor of MainActivity.cs. There you will set preferences before running/building the app.

> [!NOTE]
> If you do not set prefered starting location the app will default to the devices current location.

<br>
<img width="934" alt="AndroidSettings2" src="https://github.com/user-attachments/assets/fe8c1459-8e96-45e4-bf3a-56df9d1dd602">

8. Start the LocationsMarker. After you have set some locations start TouristGuide. Enjoy!

<br>
<br>
<h2 style="color:yellow;">Basic Use Case</h2>


<br>
<br>
1. Login to the application. Go Locations > Create New . Mark a location on the map to fill the coordinates automatically and then fill in the rest of the form.
   

<br>
<br>

![Screenshot 2024-07-28 at 15-18-27 Create - Topothesies](https://github.com/user-attachments/assets/77bfb350-9bb3-4bd4-983a-33aa4fc5cf6f)


<br>
2. Upload the sound and image files.
<br>
<br>

<img width="1454" alt="Screenshot 2024-07-28 165310" src="https://github.com/user-attachments/assets/a4fa2217-fb9d-46c4-9c01-8776ec89c281">


<br>
<br>


3. Check the location on Locations page. Make sure your soundtrack works (prefer mp3 files)

<br>
<br>

![Screenshot 2024-07-28 at 16-55-39 Index - Topothesies](https://github.com/user-attachments/assets/e9553aca-e2c3-4854-a62e-2e62453f29ee)



<br>
<br>
4. Launch the Android app (emulator or device) and open map


<br>
<br>
<img width="468" alt="Screenshot 2024-07-28 192924" src="https://github.com/user-attachments/assets/f48c2745-d57c-47f3-af74-d5df9fb10ce4">



<br>
<br>
5. Navigate near the location you entered.


<br>
<br>
<img width="448" alt="Screenshot 2024-07-28 200954" src="https://github.com/user-attachments/assets/f249f92c-0cce-49af-b8bf-9d29a43d1d21">



<br>
<br>
6. Click in the Greek flag icon. You should see the image and description you set on LocationsMarker. Press play to hear the sound recording

<br>
<br>
<img width="458" alt="Screenshot 2024-07-28 201037" src="https://github.com/user-attachments/assets/7a302b0b-1e3f-4b70-b91c-0087ea2ef541">



<br>
<br>
7. Set the location near a sight you created if you launched with android emulator. If your device's location is closer to the sight than the 'meters to notify' value, the sound will play automatically to notify the user they are near an important attraction.


<br>
<br>
<img width="433" alt="autoplay" src="https://github.com/user-attachments/assets/f5dbbce9-4e07-4ca1-ad46-aac845403c33">
