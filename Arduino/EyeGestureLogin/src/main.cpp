#include <Arduino.h>
#include "mqttManager.h"
#include "solenoid.h"
#include "settings.h"


mqttManager mqtt;
Solenoid doorOpener(pullPin);
unsigned long timer = 0;

void setup() {
    Serial.begin(115200);

    mqtt.wifiSetup();
    //mqtt.mqttSetup();
}

void loop() {
    if(millis() >= timer + 3000){
        Serial.print("pull!");
        doorOpener.pull();
        timer = millis();
    }
    doorOpener.compute();
    //mqtt.mqttRefresher();
}

