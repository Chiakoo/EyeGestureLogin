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
10. To enable the MR with passthrough for PCVR go to VIVE Business Streaming -> Settings Icon -> Graphics and enable the option MR with passthrough.
![Alt text](materials/vive_business_streaming_mr.png "Vive Business Streaming Settings")
11. You have done the setup, have fun to test the prototype!

## Settings
#### MQTT Smart Device
The MQTT Smart Device object has the setting of the passcode.  
- ```Publish Open Door```: The mqtt topic where the microcontroller listen to. Must be the same on Unity and Arduino side.
- ```Publish Mqtt Connection```: Keep this setting equal on all smart devices, used to check if the Unity Application is connected.
- ```Pass Phrase```: Password of this smart device. The passcode consits of digits of 1...9 and can have a different length. Enter the passcode as a integer list.
  ![alt text](materials/MQTTSmartDevice.png "Setting of MQTTSmartDevice")
#### MQTT Connection:
- ```Broker Address```: Address of the MQTT broker
- ```Broker Port```: Port of the MQTT broker
- ```Auto Connect```: Must be enabled to connect to Unity. Deactivate this to test the application, because it will freeze for a few seconds on start up if the MQTT broker is not reachable.
![alt text](materials/MqttUnity.png "Setting of MQTTSmartDevice")
#### LoginPoint dwell time:
#### First Digit dwell time:
#### Other Digits dwell time:
#### Validator Settings
- ```Timeout```:
- ```Skip Allowed```:
- ```Timeout```:
- ```Max Length```: Change only if, the pattern will increase in size. Must be the same size as the amout of SingleGazePoints-1.
![alt text](materials/Validator.png "Setting of MQTTSmartDevice")


## Example Broker Settings
To allow the access from other devices onto the mosquitto broker, create a mosquitto.conf file in the mosquitto installation dir with the following content:
```
listener 1883 0.0.0.0
allow_anonymous true
```