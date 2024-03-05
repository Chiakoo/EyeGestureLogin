#ifndef PROJEKT_SETTINGS_H
#define PROJEKT_SETTINGS_H

#endif //PROJEKT_SETTINGS_H

// ------------[WiFi]------------------

#define wifiSSID "Zest"
#define wifiPassword "susi7598"

// #define wifiSSID "NB-781 5508"
// #define wifiPassword "18By785/"

// ------------[MQTT]------------------
#define mqttUser ""
#define mqttPassword ""
// #define mqttServer "test.mosquitto.org"
#define mqttServer "192.168.150.189"
#define mqttServerPort 1883
#define mqttDeviceName "SolenoidMQTT"

// define topics
#define mqttOpenDoor "EyeGestureLogin/OpenDoor"
#define mqttDoorStatus "EyeGestureLogin/isDoorOpen"
#define mqttConnectionStatus "EyeGestureLogin/isConnected"

// ------------[SOLENOID]------------------
#define pullPin 14

// ------------[DEBUG]----------------
#define debug true