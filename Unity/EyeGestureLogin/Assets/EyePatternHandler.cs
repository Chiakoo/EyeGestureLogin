using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using VIVE.OpenXR.Editor;
using System.Collections.ObjectModel;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class EyePatternHandler : MonoBehaviour
{
    private bool EyePatternEnabled = false; 
    // Start is called before the first frame update
    public GameObject text;

    public GameObject cross;

    public GameObject tick;

    private SmartConnector smartConnector;

    private Coroutine currentCoroutine;
    
    [SerializeField]
    UnityEvent activateLockCollider = new UnityEvent();

    void Start()
    {
        smartConnector = GameObject.Find("SmartConnector").GetComponent<SmartConnector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enable(int id){
        if(EyePatternEnabled){
            Debug.LogWarning("EyeLoginPattern is already enabled with an other smart device!");
        }
        else if (smartConnector.GetSmartDevice(id).IsUnlocked()) {
            Debug.LogWarning("Lock is already open. No need to enter password again.");
        }
        else{
            // Debug.Log("START Enable");
            StopCoroutineIfNotNull(currentCoroutine);
            currentCoroutine = StartCoroutine(EnableRoutine(id));
        }
    }

    public void Disable(){
        Debug.Log("Disable called");
        if(EyePatternEnabled){
            for(int i = 0;  i < transform.childCount; i++){
                GameObject digit = transform.GetChild(i).gameObject;
                digit.SetActive(false);
            }
            EyePatternEnabled = false;
            activateLockCollider.Invoke();
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

    public void OnNewDigitEntered(ReadOnlyCollection<int> id){
        // Debug.Log("OnNewDigitEntered called!");
        bool found = false;
        for(int i = 0; i < transform.childCount; i++){
            Transform child = transform.GetChild(i);
            //reset dwell time to default of gaze interactor after first digit entry
            if(id.Count == 1 && Int32.Parse(child.name)>=0){
                XRSimpleInteractable interactable = child.GetComponent<XRSimpleInteractable>();
                interactable.overrideGazeTimeToSelect = false;
            }

            //mark tiles as selected
            if(Int32.Parse(child.name) == id[id.Count-1]){
                SingleGazePoint loginpoint = child.GetComponent<SingleGazePoint>();
                loginpoint.MarkAsSelected();
                found = true;
            }
            //  Debug.Log("Size: " +   id.Count + "  Selected digit: " +  id[id.Count-1]);
        }
        if(!found){
            Debug.LogWarning("Coudn't find requested entered digit!... Ignoring");
        }
    }

    IEnumerator DisableValid(){
        Disable();
        tick.SetActive(true);
        yield return new WaitForSeconds(1);
        tick.SetActive(false);
    }

    IEnumerator DisableInvalid(){
        Disable();
        cross.SetActive(true);
        yield return new WaitForSeconds(1);
        cross.SetActive(false);
    }

    IEnumerator DevicLostConnection(){
        Disable();
        cross.SetActive(false);
        tick.SetActive(false);
        text.SetActive(true);
        TextMeshPro textMesh = text.GetComponent<TextMeshPro>();
        textMesh.text = "Device lost connection!\nTry Again!";
        //var oldSize = text.Size; 
        //text.Size = 20;
        yield return new WaitForSeconds(3);
        text.SetActive(false);
        
        //text.Size = oldSize;
        
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
