# How to Add/Swap images to the app

(For example I will be using [this sterograph](http://dla.library.upenn.edu/dla/holyland/detail.html?id=HOLYLAND_lenkin_590) )

1. Add the images to the project

The first step is to add the images you need. Add a folder named with the image id number, in this case "590" inside Assets/Resources/Images. You will need to add the left and right images into this folder. The images should follow the naming convention ####_L and ####_R, for the left and right images respectively. So in the example case, 590_L.jpg adn 590_R.jpg are added to the folder Assets/Resources/Images/590.

2. Add the Audio and subtitle to the project

Audio files have the expected naming convention of ###. If you have no audio you can copy the empty.wav file located in Resources/Audio into the Assets/Resources/Audio/Audio folder and rename it ####. 

In our example case we are putting our audio file named 590.wav into the Assets/Resources/Audio/Audio folder.

Then create a subtitle file, with the naming convention of ####, and place it into the Assets/Resources/Subtitles folder. 

3. Open the script Data.cs which is located is Assets/JVR/Scripts

4. Add the image name and number to the data arrays

In the array IMAGE_NUM, add the image number at whatever index you want it to appear. In the TITLES array add the title of the image at the same index. In the example case I would push the value 590 onto the end of the IMAGE_NUM array and the title, "Picturesque Palestine--the Wilderness of the Scapegoat, Judea", to the end of the TITLES array.

5. Build & Run

