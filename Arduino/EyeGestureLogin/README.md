# EyeGestureLogin Locker

## Setup
1. Install PlatformIO for your IDE
2. Install CH340-Driver for upload to the esp32
3. Check that your cable support data transimission
4. Set the correct WIFI credentials in the settings.h file
5. Set the correct MQTT Broker in settings.h file
6. Check that the PINS of the microcontroller are correct connected: Orange: IO14 Red: VIN (power 12V) and Black: GND
7. The solenoid is placed such that the door can be opened without any power. To make it functional the solenoid has to be moved to the right and aligned parallel with the 3D printed encloser. This is done, because it would be impossible to open the door without electicity. If your are not using the demo make sure to push the solenoid back such that the door opens freely.