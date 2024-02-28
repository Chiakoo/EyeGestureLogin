#include <Arduino.h>
#include "mqttManager.h"
#include "solenoid.h"
#include "settings.h"

// put function declarations here:
int myFunction(int, int);

Solenoid doorOpener(pullPin);
unsigned long timer = 0;

void setup() {
  // put your setup code here, to run once:

  int result = myFunction(2, 3);
}

void loop() {
    if(millis() >= timer + 3000){
        Serial.print("pull!");
        doorOpener.pull();
        timer = millis();
    }
  // put your main code here, to run repeatedly:
}

// put function definitions here:
int myFunction(int x, int y) {
  return x + y;
}