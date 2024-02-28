#ifndef PROJEKT_SETTINGS_H
#define PROJEKT_SETTINGS_H

#endif //PROJEKT_SETTINGS_H

// ------------[WiFi]------------------


#define wifiSSID "Zest"
#define wifiPassword "susi7598"

// ------------[MQTT]------------------
#define mqttUser ""
#define mqttPassword ""
#define mqttServer "test.mosquitto.org"
//#define mqttServer "10.0.0.54"
#define mqttServerPort 1883
#define mqttDeviceName ""

// define topics
#define mqttSubscribeChannel "EyeGestureLogin/Test"
#define mqttPublishChannel "EyeGestureLogin/Test"
// ------------[SOLENOID]------------------
#define pullPin 14

// ------------[DEBUG]----------------
#define debug true