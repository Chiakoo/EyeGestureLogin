/*
The MIT License (MIT)

Copyright (c) 2018 Giovanni Paolo Vigano'

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;

/// <summary>
/// Examples for the M2MQTT library (https://github.com/eclipse/paho.mqtt.m2mqtt),
/// </summary>
namespace M2MqttUnity.Examples
{
    /// <summary>
    /// Script for using M2MQTT with Unity
    /// </summary>
    /// 


    public class MqttUnity : M2MqttUnityClient
    {   

        private List<string> eventMessages = new List<string>();

        // publish
        string mqttConnectionStatus = "EyeGestureLogin/Unity/isConnected" ;
        string[] mqttOpenDoor = new string[] {"EyeGestureLogin/OpenDoor"} ;
        // subscribe
        string[] mqttDoorStatus = new string[] {"EyeGestureLogin/isDoorOpen"} ;
       

        protected override void OnConnecting()
        {
            Debug.Log("Connecting to broker on " + brokerAddress + ":" + brokerPort.ToString() + "...\n");
        }

        protected override void OnConnected()
        {
            // base.OnConnected();
            Debug.Log("Connected to broker on " + brokerAddress + "\n");

            // Subscribe to topics
            SubscribeTopic(mqttDoorStatus);
            client.Publish(mqttConnectionStatus, System.Text.Encoding.UTF8.GetBytes("CONNECTED"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        protected void SubscribeTopic(string[] topic)
        {
            client.Subscribe(topic, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        protected void UnsubscribeTopic(string[] topic)
        {
            client.Unsubscribe(topic);
        }

        protected override void OnConnectionFailed(string errorMessage)
        {
            Debug.Log("CONNECTION FAILED! " + errorMessage);
        }

        protected override void OnDisconnected()
        {
            Debug.Log("Disconnected.");
        }

        protected override void OnConnectionLost()
        {
            Debug.Log("CONNECTION LOST!");
        }

        protected override void Start()
        {
            Debug.Log("Ready.");
            base.Start();
        }

        protected override void DecodeMessage(string topic, byte[] message)
        {
            string msg = System.Text.Encoding.UTF8.GetString(message);
            Debug.Log("[" + topic + "]: " + msg);
            StoreMessage(msg);
        }

        // needed?
        private void StoreMessage(string eventMsg)
        {
            eventMessages.Add(eventMsg);
        }

        // needed?
        private void ProcessMessage(string msg)
        {
            Debug.Log("Received: " + msg);
        }

        protected override void Update()
        {
            base.Update(); // call ProcessMqttEvents()
        }

        private void OnDestroy()
        {
            Disconnect();
        }

        private void OnValidate()
        {
            Debug.Log("deleted content");
        }
    }
}
