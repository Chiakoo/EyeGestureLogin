//
// Created by tobia on 23.05.2023.
//

#ifndef ADVANCEDGRAVITYGLOVE_SOLENOID_H
#define ADVANCEDGRAVITYGLOVE_SOLENOID_H

#include <Arduino.h>
#include "PubSubClient.h"


class Solenoid {
public:
    Solenoid(int pullPin, int duration) : pullPin(pullPin),  duration(duration){
        pinMode(pullPin, OUTPUT);
    }

    void pull(); //closes the solenoid
    void pull(int delay);

    String compute(); //Has to be called every loop
private:
    int pullPin;
    int duration;       // The time the solenoid stays activated [ms]
    const int peakTime = 100; //The time the solenoid needs to be powered to change states [ms]
    int delay_push, delay_pull = 0;
    unsigned long timer_start, timer_push_start, timer_pull_start = 0;
    bool timer, timer_push, timer_pull = false;
};


#endif //ADVANCEDGRAVITYGLOVE_SOLENOID_H
