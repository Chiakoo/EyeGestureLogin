//
// Created by tobia on 23.05.2023.
//

#ifndef ADVANCEDGRAVITYGLOVE_SOLENOID_H
#define ADVANCEDGRAVITYGLOVE_SOLENOID_H

#include <Arduino.h>


class Solenoid {
public:
    Solenoid(int pullPin) : pullPin(pullPin) {
        pinMode(pullPin, OUTPUT);
    }

    void pull(); //closes the solenoid
    void pull(int delay);

    void compute(); //Has to be called every loop
private:
    int pullPin;
    const int peakTime = 100; //The time the solenoid needs to be powered to change states.
    int delay_push, delay_pull = 0;
    unsigned long timer_start, timer_push_start, timer_pull_start = 0;
    bool timer, timer_push, timer_pull = false;
};


#endif //ADVANCEDGRAVITYGLOVE_SOLENOID_H
