using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

/// <summary>
/// Class <c>SingleGazePoint</c> represents a single login field of the pattern and informs the validator on sucessfull selection.
/// </summary>
public class SingleGazePoint : MonoBehaviour
{
    public SmartConnector smartConnector;

    public Color initColor;

    private Color lastColor;

    private Color selectedColor = Color.green;

    private Color hoveredColor = Color.cyan;

    private Validator validator;
    public int deviceId {set; get;}
    // Start is called before the first frame update
    void Start()
    {
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
        List<GameObject> childs = new List<GameObject>();
        gameObject.GetChildGameObjects(childs);
        foreach(var child in childs){
            child.GetComponent<Renderer>().material.color = hoveredColor; 
        }
    }

    public void OnHoverExit(){
        Material material = this.gameObject.GetComponent<Renderer> ().material;
        //keep old selected color
        if(material.color.Equals(selectedColor)){
            return;
        }
        Color newColor = lastColor; //eventuell referenz
        lastColor = material.color;
        material.color = newColor;

        //update the color of all childs of objects
        List<GameObject> childs = new List<GameObject>();
        gameObject.GetChildGameObjects(childs);
        foreach(var child in childs){
            child.GetComponent<Renderer>().material.color = newColor; 
        }
    }

    public void Selected(){
        Material material = this.gameObject.GetComponent<Renderer> ().material;
        //Debug.Log("Selected by gaze (" + loginPointObj.name+")");
        lastColor = material.color;
        material.color = selectedColor;
        // Debug.Log("Name:" + this.gameObject.name);
        if(this.gameObject.name.Equals("-1")){
        //    Debug.Log("Canceled");
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

        List<GameObject> childs = new List<GameObject>();
        gameObject.GetChildGameObjects(childs);
        foreach(var child in childs){
            child.GetComponent<Renderer>().material.color = initColor; 
        }
    }
}
