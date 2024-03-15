using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using M2MqttUnity.Examples;
using UnityEngine;

[System.Serializable]
public class MQTTSmartDevice : SmartDevice
{

    public MqttUnity mqtt;

    public List<int> passPhrase = new List<int> {1,2,3,6};

    //TODO add Topics here

        // Start is called before the first frame update
    void Start()
    {
        //mqtt = transform.GetChild(0).GetComponent<MqttUnity>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool IsAvailable()
    {
        return mqtt.IsConnected;
    }

    public override bool IsUnlocked()
    {
        throw new System.NotImplementedException();
    }

    public override bool Lock()
    {
        throw new System.NotImplementedException();
    }

    public override int RemainingUnlockTimeInSeconds()
    {
        throw new System.NotImplementedException();
    }

    public override bool Unlock()
    {
        mqtt.PublishTopic(mqtt.mqttOpenDoor[0], "true");
        return true;
    }

    public override ReadOnlyCollection<int> GetPassphrase()
    {
        return passPhrase.AsReadOnly();
    }
}
