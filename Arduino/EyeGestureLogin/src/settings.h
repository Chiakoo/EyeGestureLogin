#ifndef PROJEKT_SETTINGS_H
#define PROJEKT_SETTINGS_H

#endif //PROJEKT_SETTINGS_H

// ------------[WiFi]------------------

#define wifiSSID "TODO"
#define wifiPassword "TODO"

// ------------[MQTT]------------------
#define mqttUser ""
#define mqttPassword ""
#define mqttServer "192.168.11.2" //TODO
#define mqttServerPort 1883
#define mqttDeviceName "SolenoidMQTT"

// define topics
#define mqttOpenDoor "EyeGestureLogin/0/OpenDoor"
#define mqttDoorStatus "EyeGestureLogin/0/isDoorOpen"
#define mqttConnectionStatus "EyeGestureLogin/0/isConnected"

// ------------[SOLENOID]------------------
#define pullPin 14

// ------------[DEBUG]----------------
#define debug true