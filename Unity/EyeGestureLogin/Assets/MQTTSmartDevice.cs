using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using M2MqttUnity.Examples;
using UnityEngine;

[System.Serializable]
public class MQTTSmartDevice : SmartDevice
{

    public MqttUnity mqtt;
    private LockHandler virtualLockHandler;
    private string lockName = "Lock";
    // publish
    [SerializeField]
    private string publishOpenDoor = "EyeGestureLogin/0/OpenDoor";    
    [SerializeField]
    private string publishMqttConnection = "EyeGestureLogin/Unity/isConnected";

    // subscribe
    private const string subscribeDoorStatus = "EyeGestureLogin/0/isDoorOpen";
    private const string subscribeArduinoStatus = "EyeGestureLogin/0/isConnected";

    public List<int> passPhrase = new List<int> {1,2,3,6};
    private bool publishedConnection = false;

    void Start()
    {
        //mqtt = transform.GetChild(0).GetComponent<MqttUnity>();
        findLock();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAvailable() && !publishedConnection) {
            Debug.Log("Subscribe from MQTT device");
            mqtt.PublishTopic(publishMqttConnection, "CONNECTED");
            mqtt.SubscribeTopic(new string[] {subscribeDoorStatus, subscribeArduinoStatus}, this);
            publishedConnection = true;
        }
    }

    void findLock() {
        for (int index=0; index < this.transform.childCount; index ++) {
            if (this.transform.GetChild(index).name == lockName) {
                virtualLockHandler = this.transform.GetChild(index).GetComponent<LockHandler>();
                return;
            }
        }
        Debug.LogWarning("Did not find the lock of this SmartDevice");
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

    public void LockVirtualLock() {
        virtualLockHandler.CloseLock();
    }

    public void OpenVirtualLock() {
        virtualLockHandler.OpenLock();
    }

    public override int RemainingUnlockTimeInSeconds()
    {
        throw new System.NotImplementedException();
    }

    public override bool Unlock()
    {
        mqtt.PublishTopic(publishOpenDoor, "true");
        return true;
    }

    public override ReadOnlyCollection<int> GetPassphrase()
    {
        return passPhrase.AsReadOnly();
    }

    public void OnReceiveTopic(string topic, string message) {
        Debug.Log("Received Topic: " + topic + " Message: " + message);
        switch (topic) {
            case subscribeArduinoStatus:
                break;
            case subscribeDoorStatus:
                
                if (message == "open") {
                    OpenVirtualLock();
                }
                else if (message == "closed") {
                    LockVirtualLock();
                }
                break;
            default: 
                break;
        }
    }
}
