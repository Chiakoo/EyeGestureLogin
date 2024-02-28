#include "solenoid.h"

void Solenoid::pull() {
    digitalWrite(pullPin, true);
    timer_start = millis();
    timer = true;
}

void Solenoid::compute() {
    if (timer && millis() - timer_start > peakTime) {
        timer = false;
        digitalWrite(pullPin, false);
    }
    if (timer_pull && millis() - timer_pull_start > delay_pull) {
        timer_pull = false;
        pull();
    }
}

void Solenoid::pull(int delay) {
    delay_pull = delay;
    timer_pull = true;
}
