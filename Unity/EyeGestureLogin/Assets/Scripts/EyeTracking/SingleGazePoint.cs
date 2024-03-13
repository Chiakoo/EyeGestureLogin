using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit;

public class SingleGazePoint : MonoBehaviour
{

    public GameObject loginPointObj;

    public SmartConnector smartConnector;

    private Color initColor;

    public int deviceId {set; get;}
    // Start is called before the first frame update
    void Start()
    {
        initColor = loginPointObj.GetComponent<Renderer> ().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHoverEnter(){
        Debug.Log("hover entered");
    }

    public void Selected(){
        Debug.Log("Selected by gaze");
        loginPointObj.GetComponent<Renderer> ().material.color = Color.green;
        Debug.Log("NAme:" + loginPointObj.name);
        int enteredNumber = Int32.Parse(loginPointObj.name);
        if(loginPointObj.name.Equals("Cube (2)")){
            Debug.Log("Unlock solenoid");
            smartConnector.GetSmartDevice(0).Unlock();
        }
    }

    public void Deselected(){
        Debug.Log("Deselected by gaze!");
        loginPointObj.GetComponent<Renderer> ().material.color = initColor;
    }
}
