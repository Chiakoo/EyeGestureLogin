using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit;

public class SingleGazePoint : MonoBehaviour
{
    public SmartConnector smartConnector;

    private Color initColor;

    private Color lastColor;

    private Color selectedColor = Color.green;

    private Color hoveredColor = Color.cyan;

    private Validator validator;
    public int deviceId {set; get;}
    // Start is called before the first frame update
    void Start()
    {
        initColor = this.gameObject.GetComponent<Renderer> ().material.color;
        validator = GameObject.Find("Validator").GetComponent<Validator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MarkAsSelected(){
        Material material = this.gameObject.GetComponent<Renderer> ().material;
        lastColor = material.color;
        material.color = selectedColor;
    }

    public void OnHoverEnter(){
        Material material = this.gameObject.GetComponent<Renderer> ().material;
        if(material.color.Equals(selectedColor)){
            return;
        }
        lastColor = material.color;
        material.color = hoveredColor;
    }

    public void OnHoverExit(){
        Material material = this.gameObject.GetComponent<Renderer> ().material;
        //keep old selected color
        if(material.color.Equals(selectedColor)){
            return;
        }
        Color newColor = lastColor; //eventuell referenz
        material.color = newColor;
        lastColor = material.color;
    }

    public void Selected(){
        Material material = this.gameObject.GetComponent<Renderer> ().material;
        //Debug.Log("Selected by gaze (" + loginPointObj.name+")");
        lastColor = material.color;
        material.color = selectedColor;
        Debug.Log("Name:" + this.gameObject.name);
        if(this.gameObject.name.Equals("cancel")){
            Debug.Log("will cancel!");
            validator.CancelEntry();
            return;
        }
        int enteredNumber = Int32.Parse(this.gameObject.name);
        validator.NewDigit(deviceId, enteredNumber);
    }

    public void Deselected(){
        //Debug.Log("Deselected by gaze (" + loginPointObj.name+")");
        //loginPointObj.GetComponent<Renderer> ().material.color = initColor;
    }

    public void ResetToDefault(){
        this.gameObject.GetComponent<Renderer> ().material.color = initColor;
        this.gameObject.GetComponent<XRSimpleInteractable>().overrideGazeTimeToSelect = true;
    }
}
