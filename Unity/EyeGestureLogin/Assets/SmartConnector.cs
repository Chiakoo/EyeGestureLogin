using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using M2MqttUnity.Examples;
using UnityEditor;
using System;

public class SmartConnector : MonoBehaviour
{
    // Start is called before the first frame update

    public List<SmartDevice> smartDevices;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SmartDevice GetSmartDevice(int id){
        return smartDevices[id];
    }
}
