using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using VIVE.OpenXR.Editor;

public class EyePatternHandler : MonoBehaviour
{
    private bool EyePatternEnabled = false; 
    // Start is called before the first frame update
    public GameObject text;

    private Coroutine currentCoroutine;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enable(int id){
        if(EyePatternEnabled){
            Debug.LogWarning("EyeLoginPattern is already enabled with an other smart device!");
        }else{
            // Debug.Log("START Enable");
            StopCoroutineIfNotNull(currentCoroutine);
            currentCoroutine = StartCoroutine(EnableRoutine(id));
        }
    }

    public void Disable(){
        // Debug.Log("Disable called");
        if(EyePatternEnabled){
            for(int i = 0;  i < transform.childCount; i++){
                GameObject digit = transform.GetChild(i).gameObject;
                digit.SetActive(false);
            }
            EyePatternEnabled = false;
        }
    }

    private void StopCoroutineIfNotNull(Coroutine coroutine)
        {
            if( coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }

    public void OnValidPassphrase(){
        StopCoroutineIfNotNull(currentCoroutine);
        currentCoroutine = StartCoroutine(DisableValid());
    }

    public void OnInvalidPassphrase(){
        StopCoroutineIfNotNull(currentCoroutine);
        currentCoroutine = StartCoroutine(DisableInvalid());
        //todo inform user wrong password
    }

    public void OnDeviceLostConnection(){
        StopCoroutineIfNotNull(currentCoroutine);
        currentCoroutine = StartCoroutine(DevicLostConnection());
    }

    public void OnNewDigitEntered(int id){
        bool found = false;
        foreach(GameObject child in transform){
                if(Int32.Parse(child.name) == id){
                    SingleGazePoint loginpoint = child.GetComponent<SingleGazePoint>();
                    loginpoint.MarkAsSelected();
                    found = false;
                }
            }
        if(!found){
            Debug.LogWarning("Coudn't find requested entered digit!... Ignoring");
        }
    }

    IEnumerator DisableValid(){
        Disable();
        text.SetActive(true);
        TextMeshPro textMesh = text.GetComponent<TextMeshPro>();
        textMesh.text = "Successfull!";
        yield return new WaitForSeconds(3);
        text.SetActive(false);
    }

    IEnumerator DisableInvalid(){
        Disable();
        text.SetActive(true);
        TextMeshPro textMesh = text.GetComponent<TextMeshPro>();
        textMesh.text = "Invalid Pass!\nTry Again!";
        yield return new WaitForSeconds(3);
        text.SetActive(false);
    }

    IEnumerator DevicLostConnection(){
        Disable();
        text.SetActive(true);
        TextMeshPro textMesh = text.GetComponent<TextMeshPro>();
        textMesh.text = "Device lost connection!\nTry Again!";
        yield return new WaitForSeconds(3);
        text.SetActive(false);
        
    }
    
    IEnumerator EnableRoutine(int id){
        EyePatternEnabled = true;
        // Debug.Log("In Enable routine!");
        text.SetActive(true);
        TextMeshPro textMesh = text.GetComponent<TextMeshPro>();
        textMesh.text = "Start \n2";
        yield return new WaitForSeconds(1);
        textMesh.text = "Start \n1";
        yield return new WaitForSeconds(1);
        text.SetActive(false);
        for(int i = 0;  i < transform.childCount; i++){
                GameObject digit = transform.GetChild(i).gameObject;
                SingleGazePoint singleGazePoint = digit.GetComponent<SingleGazePoint>();
                singleGazePoint.deviceId = id;
                singleGazePoint.ResetToDefault();
                digit.SetActive(true);
        }
    }
}
