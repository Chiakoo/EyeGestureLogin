#include <Arduino.h>
#include "mqttManager.h"
#include "solenoid.h"
#include "settings.h"


mqttManager mqtt;
Solenoid doorOpener(pullPin, 5000);
unsigned long timer = 0;

String currTopic;
String currContent;

void setup() {
    // Serial.begin(115200);
    Serial.begin(9600);

    mqtt.wifiSetup();
    mqtt.mqttSetup();
}

void loop() {
    // if(millis() >= timer + 3000){
    //     Serial.println("pull!");
    //     doorOpener.pull();
    //     timer = millis();
    // }
    mqtt.mqttRefresher();
    
    // new callback available?
    if (mqtt.checkCallback(&currTopic, &currContent)) {
        // openDoor topic
        if (currTopic == mqttOpenDoor) {
            Serial.println("received OpenDoor");
            if (currContent == "true") {
                doorOpener.pull();
                mqtt.publishTopic(mqttDoorStatus, "open");
            }
        }
    }

    if (doorOpener.compute() == "closed") {
        Serial.println("publish: closed door");
        mqtt.publishTopic(mqttDoorStatus, "closed");
    }
}

