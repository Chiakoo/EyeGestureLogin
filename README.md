# EyeGestureLogin

## Setup
1. Install Unity
2. Open in Unity the project ./Unity/EyeGestureLogin
3. Install a MQTT Broker e.g Mosquitto on a device that is reachable from the microcontroller and the unity application
4. Configure the Mosquitto (make sure that it is reachable from other devices; an example configuration is found at the bottom of this readme)
5. Look at the readme for the microcontroller at ./Arduino/EyeGestureLogin/README.md and perform the described steps there
6. Go back into the Unity scene (Assets/Scenes/EyeGestureLoginDemo)
7. Set the MQTT configuration at Unity in GameObjects Hierarchy. To that navigate to "Smart Devices/Connectors/MQTT Connection"
8. At MQTT connection set the MQTT credentials and the correct port and IP adress of the Mosquitto Broker
9. For the VIVE XR Elite Headset install VIVE Business Streaming
10. To enable the MR with passthrough for PCVR go to VIVE Business Streaming -> Settings Icon -> Graphics and enable the option MR with passthrough
11. You have done the setup, have fun to test the prototype!

## Settings
#### MQTT:
#### LoginPoint dwell time:
#### First Digit dwell time:
#### Other Digit dwell time:
#### Validator Settings
Timeout...
