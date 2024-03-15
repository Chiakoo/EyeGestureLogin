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

    private Validator validator;
    public int deviceId {set; get;}
    // Start is called before the first frame update
    void Start()
    {
        initColor = loginPointObj.GetComponent<Renderer> ().material.color;
        validator = GameObject.Find("Validator").GetComponent<Validator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHoverEnter(){
    }

    public void Selected(){
        //Debug.Log("Selected by gaze (" + loginPointObj.name+")");
        loginPointObj.GetComponent<Renderer> ().material.color = Color.green;
        //Debug.Log("NAme:" + );
        int enteredNumber = Int32.Parse(loginPointObj.name);
        validator.NewDigit(deviceId, enteredNumber);
    }

    public void Deselected(){
        //Debug.Log("Deselected by gaze (" + loginPointObj.name+")");
        //loginPointObj.GetComponent<Renderer> ().material.color = initColor;
    }

    public void ResetToDefault(){
        loginPointObj.GetComponent<Renderer> ().material.color = initColor;
    }
}
