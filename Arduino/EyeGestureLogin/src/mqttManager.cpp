#include "mqttManager.h"
#include "PubSubClient.h"
#include <WiFiClient.h>
#include <WiFi.h>
#include "settings.h"
#include <list>
#include <string>
#include <optional>
#include <iostream>

void wifiSetup();

void mqttRefresher();
void mqttSetup();
boolean reconnect();
void readInputFromConsole();

WiFiClient espClient;
PubSubClient mqttClient(espClient);
unsigned long lastReconnectAttempt = 0;

// create a list of 2x charcter type
// string, bool (?)
std::list <std::pair< String, String >> callbacks = {std::make_pair("topic", "value")};

void mqttManager::mqttSetup(){
    // Do not use to long names here, the default maximum package size is 128 bytes
    mqttClient.setServer(mqttServer, mqttServerPort);
    using std::placeholders::_1;
    using std::placeholders::_2;
    using std::placeholders::_3;
    mqttClient.setCallback(std::bind(&mqttManager::callback, this, _1,_2,_3));
}

void mqttManager::mqttRefresher() {
    if (!mqttClient.connected()) {
        if(debug) {
            Serial.println("MQTT lost connection, try to reconnect!");
        }
        if (millis() - lastReconnectAttempt > 5000) {
            lastReconnectAttempt = millis();
            // Attempt to reconnect
            if (reconnect()) {
                lastReconnectAttempt = 0;
            }
        }
    } else {
        // Client connected
        mqttClient.loop();
    }
}


boolean mqttManager::reconnect() {
    if (mqttClient.connect(mqttDeviceName, mqttUser, mqttPassword)) {
        mqttClient.subscribe(mqttOpenDoor);
        mqttClient.subscribe(mqttDoorStatus);
        mqttClient.subscribe(mqttConnectionStatus);
        mqttClient.publish(mqttConnectionStatus, "CONNECTED");
        Serial.println("MQTT connected");
    }
    return mqttClient.connected();
}


// called when message arrives for a subscription of this client
void mqttManager::callback(char *topic, uint8_t *payload, unsigned int length) {
  
    String msg = "";
    for (int i = 0; i < length; i++) {
        msg.concat((char)payload[i]);
        //Serial.print((char)payload[i]);
    }

    payload[length] = '\0';
    String content = String((char*)payload);

    Serial.print("[" );
    Serial.print(topic);
    Serial.print("]: ");
    Serial.println(content);
    callbacks.push_back(std::make_pair(topic, content));
}

bool mqttManager::checkCallback(String* topic, String* content) {
    if (!mqttClient.connected()) return false;
    
    if (callbacks.size() > 0) {
        std::pair<String, String> currentCallback = callbacks.front();
        *topic = currentCallback.first;
        *content = currentCallback.second;
        callbacks.pop_front();
        // Serial.print("Size List: ");
        // Serial.println(callbacks.size());
        return true;
    }
    
    // empty list
    return false;
}


void mqttManager::wifiSetup() {
    delay(10);
    WiFi.mode(WIFI_STA);
    WiFi.begin(wifiSSID, wifiPassword);
    while (WiFi.status() != WL_CONNECTED) {
        delay(500);
        if(debug){
            Serial.print(".");
        }
    }
    if(debug){
        Serial.println("");
        Serial.println("WiFi is established");
        Serial.println("My IP address is: ");
        Serial.println(WiFi.localIP());
    }
}

void mqttManager::publishTopic (const char *topic, const char *message) {
    mqttClient.publish(topic, message);
    Serial.print("Manager published: ");
    Serial.println(topic);
}

mqttManager::mqttManager() {

}
