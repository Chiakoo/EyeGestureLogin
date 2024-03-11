using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using M2MqttUnity.Examples;
using UnityEditor;
using System;

public class SmartConnector : MonoBehaviour
{
    // Start is called before the first frame update
    public MqttUnity mqtt;

    void Start()
    {
        mqtt = GameObject.Find("MQTT Connection").GetComponent<MqttUnity>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock(){
        mqtt.PublishTopic(mqtt.mqttOpenDoor[0], "true");
    }

    public bool IsUnlocked(){
        throw new NotSupportedException();
        return false;
    }

    public void Lock(){
        throw new NotSupportedException();
    }
}
