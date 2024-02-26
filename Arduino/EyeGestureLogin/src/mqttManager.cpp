#include "mqttManager.h"

void wifiSetup();

void mqttRefresher();
void mqttSetup();
boolean reconnect();
void callback(char *topic, byte *payload, int length);

void readInputFromConsole();