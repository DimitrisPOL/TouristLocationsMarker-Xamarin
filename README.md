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




## Getting Started

### Clone the Repository

```bash
git clone https://github.com/DimitrisPOL/TouristLocationsMarker.git
cd TouristLocationsMarker

## Setting Up the Project
Install Required Packages

Ensure all necessary packages are installed:

dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add Microsoft.AspNetCore.Identity.EntityFrameworkCore

Configure the Database Context

PM> Enable-Migrations

PM> Update-Database

```

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



<h2>Setup Instructions</h2>


<br>
<br>
1. Set db connection string and prefered map starting coordinates on Location.UI appsettings.developments.json
   

<br>
<br>
<img width="833" alt="appsettings" src="https://github.com/user-attachments/assets/771e0f07-d5bc-43f2-8b1f-b1e02ae3b217">



<br>
<br>
2. If you are launching via emulator you will need to bind your localhost with 127.0.0.1 . This is the default IP which Android emulators set for the localhost of the hosting machine. The Android app will not be able to see your localhost/port if this is not set.

   
<br>
<br>

<img width="979" alt="hostconfig" src="https://github.com/user-attachments/assets/df45d2c9-446c-4e89-99ae-eea1c07ee6e6">


<br>
<br>
3.Navigate at Locations.UI\Pages\Shared\_Layout.cshtml and fill in a valid Google Map Api Key. Visit this [Page](https://developers.google.com/maps/documentation/javascript/get-api-key) for more informtaion.

<br>
<br>

![googleapikey](https://github.com/user-attachments/assets/d4fa15db-1b03-4973-abf5-341bfe378704)

<br>
<br>

4. Enable hardware acceleration in your pc. See [here](https://learn.microsoft.com/en-us/dotnet/maui/android/emulator/hardware-acceleration?view=net-maui-8.0) for more information


<br>
<br>

5. In the Android applicaton navigate to the constructor of MainActivity.cs. There you will set preferences before running/building the app.

Note: If you do not set prefered starting location the app will default to the devices current location.

<br>
<br>
<img width="934" alt="AndroidSettings2" src="https://github.com/user-attachments/assets/fe8c1459-8e96-45e4-bf3a-56df9d1dd602">



<br>
<br>
6. Start the LocationsMarker. After you have set some locations start TouristGuide. Enjoy!


<br>
<br>
