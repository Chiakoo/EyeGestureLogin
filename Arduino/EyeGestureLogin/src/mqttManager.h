#ifndef PROJEKT_MQTTMANAGER_H
#define PROJEKT_MQTTMANAGER_H

#include "PubSubClient.h"

void wifiSetup();

void mqttRefresher();
void mqttSetup();
boolean reconnect();
void callback(char *topic, byte *payload, int length);


class mqttManager {
    public:
        mqttManager();
        void wifiSetup();
        void mqttRefresher();
        void mqttSetup();
        boolean reconnect();
        void callback(char *topic, byte *payload, int length);
};

#endif
