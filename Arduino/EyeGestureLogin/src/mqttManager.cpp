#include "mqttManager.h"
#include "PubSubClient.h"
#include <WiFiClient.h>
#include <WiFi.h>
#include "settings.h"

void wifiSetup();

void mqttRefresher();
void mqttSetup();
boolean reconnect();
void callback(char *topic, byte *payload, int length);

void readInputFromConsole();

WiFiClient espClient;
PubSubClient mqttClient(espClient);
unsigned long lastReconnectAttempt = 0;

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

void callback(char *topic, byte *payload, unsigned int length) {
    String msg = "";
    for (int j = 0; j < length; j++) {
        msg.concat((char)payload[j]);
    }
    //myMorseAnalyzer->setWord(msg, true);
}

void mqttManager::mqttSetup(){
    // Do not use to long names here, the default maximum package size is 128 bytes
    mqttClient.setServer(mqttServer, mqttServerPort);
    //mqttClient.setCallback(callback);
}

boolean mqttManager::reconnect() {
    if (mqttClient.connect(mqttDeviceName, mqttUser, mqttPassword)) {
        mqttClient.subscribe(mqttSubscribeChannel);
        mqttClient.publish(mqttSubscribeChannel, "CONNECTED");
    }
    return mqttClient.connected();
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

mqttManager::mqttManager() {

}
