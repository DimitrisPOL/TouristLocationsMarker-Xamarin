TouristGuide & LocationsApi Overview

TouristGuide is an Android application developed with Xamarin Android. It allows users to explore tourist attractions in Greece by displaying them on a map with markers. Users can click on a marker to view an image, read a description, and play a sound recording describing the sight. Additionally, if a user's current location is close to the sight, the sound recording will play automatically.


LocationsMarker Api is a web application that enables administrators to create and manage tourist attraction locations that will be used be TouristGuide app to present the sights to the users. Administrators can provide information such as the name, area, postal code, description, image, and sound recording for each location. They can also set the distance at which the sound recording will play automatically for the Android application users. 

Basic Use Case

1. Login to the application. Go Locations > Create New . Mark a location on the map to fill the coordinates automatically and then fill in the rest of the form.

![Screenshot 2024-07-28 at 15-18-27 Create - Topothesies](https://github.com/user-attachments/assets/77bfb350-9bb3-4bd4-983a-33aa4fc5cf6f)

2. Upload the sound and image files.

<img width="1454" alt="Screenshot 2024-07-28 165310" src="https://github.com/user-attachments/assets/a4fa2217-fb9d-46c4-9c01-8776ec89c281">

3. Check the location on Locations page. Make sure your soundtrack works (prefer mp3 files)

![Screenshot 2024-07-28 at 16-55-39 Index - Topothesies](https://github.com/user-attachments/assets/e9553aca-e2c3-4854-a62e-2e62453f29ee)

4. Launch the Android app (emulator or device) and open map
<img width="468" alt="Screenshot 2024-07-28 192924" src="https://github.com/user-attachments/assets/f48c2745-d57c-47f3-af74-d5df9fb10ce4">


5. Navigate near the location you entered.
<img width="448" alt="Screenshot 2024-07-28 200954" src="https://github.com/user-attachments/assets/f249f92c-0cce-49af-b8bf-9d29a43d1d21">


6. Click in the Greek flag icon. You should see the image and description you set on LocationsMarker. Press play to hear the sound recording

<img width="458" alt="Screenshot 2024-07-28 201037" src="https://github.com/user-attachments/assets/7a302b0b-1e3f-4b70-b91c-0087ea2ef541">


8. Create a location near your location if you launched on mobile devices or set the location near a sight you created if you launched with android emulator. If your device is closer to the sight than the 'meters to notify' value, the sound will play automatically to notify the user they are near an important attraction
