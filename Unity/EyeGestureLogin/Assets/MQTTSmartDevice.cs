using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using M2MqttUnity.Examples;
using UnityEngine;

public class MQTTSmartDevice : MonoBehaviour, SmartDevice
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

    public bool IsAvailable()
    {
        throw new System.NotImplementedException();
    }

    public bool IsUnlocked()
    {
        throw new System.NotImplementedException();
    }

    public bool Lock()
    {
        throw new System.NotImplementedException();
    }

    public int RemainingUnlockTimeInSeconds()
    {
        throw new System.NotImplementedException();
    }

    public bool Unlock()
    {
        mqtt.PublishTopic(mqtt.mqttOpenDoor[0], "true");
        return true;
    }

    public ReadOnlyCollection<int> GetPassphrase()
    {
        return passPhrase.AsReadOnly();
    }
}
