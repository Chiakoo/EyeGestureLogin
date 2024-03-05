#include "solenoid.h"

void Solenoid::pull() {
    digitalWrite(pullPin, true);
    timer_start = millis();
    timer = true;
    Serial.println("Solenoid: opened door");
}

String Solenoid::compute() {
    if (timer) {
        // close door
        if (millis() - timer_start > peakTime + duration) {
            timer = false;
            digitalWrite(pullPin, false);
            Serial.println("Solenoid: closed door");
            return "closed";
        }
        // door still open
        Serial.println("Door is still open");
        return "open";
    }
    
    if (timer_pull) {
        // opening the door
        if (millis() - timer_pull_start > delay_pull) {
            timer_pull = false;
            pull();
            Serial.println("Opening the door delayed");
            return "open";
        }
        // waiting for opening the door
        Serial.println("waiting for opening the door");
        return "closed";
    }
    
    // no current request - default closed
    return "nothing";
}

void Solenoid::pull(int delay) {
    delay_pull = delay;
    timer_pull = true;
}
