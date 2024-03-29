#ifndef PROJEKT_MQTTMANAGER_H
#define PROJEKT_MQTTMANAGER_H

#include "PubSubClient.h"

void wifiSetup();

void mqttRefresher();
void mqttSetup();
boolean reconnect();
void callback(char *topic, byte *payload, int length);


class mqttManager {
    private: 
        void callback(char *topic, uint8_t *payload, unsigned int length);

    public:
        mqttManager();
        void wifiSetup();
        void mqttRefresher();
        void mqttSetup();
        boolean reconnect();
        void publishTopic(const char *topic, const char *message);
        bool checkCallback(String* topic, String* content);
};

#endif
