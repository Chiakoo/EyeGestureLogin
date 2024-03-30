using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using M2MqttUnity.Examples;
using UnityEditor;
using System;

/// <summary>
/// Class <c>SmartConnector</c> handels all smart devices. Every smart device is identified by the smart connector by its index in the m_smartDevices list. They have to be set by unity.
/// </summary>
public class SmartConnector : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeReference]
    public List<SmartDevice> m_smartDevices = new List<SmartDevice>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SmartDevice GetSmartDevice(int id){
        return m_smartDevices[id];
    }
}
