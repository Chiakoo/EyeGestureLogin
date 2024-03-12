using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using M2MqttUnity;
using M2MqttUnity.Examples;
using System.Runtime.InteropServices;
using System;
using UnityEngine.Events;

public class Validator : MonoBehaviour
{
    [Header("Validator Settings")]
    [Tooltip("true: 1,3 valid. false: 1,3 becomes 1,2,3")]
    public bool skipAllowed = false;
    [Tooltip("seconds without input until timeout triggered/ password entry ends")]
    [Range(1, 10)]
    public int timeout = 5;
    [Tooltip("maximum password length possible (all fields)")]
    public int maxLength = 5;

    [Header("Unity Events")]
    [Tooltip("Access: ValidPasswordEvent.AddListener(validPasswordReceived())")]
    [SerializeField]
    private UnityEvent ValidPasswordEvent = new UnityEvent();
    [SerializeField]
    private UnityEvent InvalidPasswordEvent = new UnityEvent();

    // internal variables
    private int currDeviceID = -1;  
    // dictionary which checks for skipped digits
    Dictionary<int, Dictionary<int, int>> skippable_dict = new Dictionary<int, Dictionary<int, int>>();
    // the correct password
    private  List<int> password = new  List<int> {1, 2, 3, 4};
    // the so far entered PIN
    private List<int> PIN = new List<int>();
    // is the start condition (first digit=5) already fulfilled?
    private bool validStart = false;
    // open door mqtt topic 
    private MqttUnity mqtt;
    private string openDoor = "EyeGestureLogin/OpenDoor";
    // is test already done?
    private bool testDone = true;
    private int lastTimestamp = 0;

    // SmartConnector smartConnector;
    

    // Start is called before the first frame update
    void Start()
    {
        password =  new List<int>{1, 2, 3, 4};
        mqtt = GameObject.Find("MQTT").GetComponent<MqttUnity>();
        // smartConnector = GameObject.Find("SmartConnector").GetComponent<SmartConnector>();

        createSkippableDictionary();
    }

    // Update is called once per frame
    void Update()
    {
        if (mqtt.IsConnected && !testDone) {
            runTest1();
        }

        if (mqtt.IsConnected && PIN.Count > 0) {
            // timeout
            if (DateTime.Now.Second > lastTimestamp + timeout) {
                Debug.Log("password timeout");
                checkPIN();
            }
            // reached maximum length
            else if (PIN.Count >= maxLength) {
                Debug.Log("max Length reached");
                checkPIN();
            }
        }
    }


    public void NewDigit(int ID, int digit) 
    {
        // trying to access different device
        if (currDeviceID != ID) {
            resetPIN();
            currDeviceID = ID;
            // get password of new device from SmartConnector 
            // password = smartConnector.getPassword(currID);
        }
       
        // the start condition is not fulfilled yet
        // --> 5
        if (!validStart) {
            if (digit == 5) {
                validStart = true;
                Debug.Log("start condition check");
                return;
            }
            // discard digit
            Debug.Log("discarded digit");
            return;
        }
        else {
            // if digit already in PIN -> return
            if(PIN.Contains(digit)) return;
            
            // if skipping is not allowed, check if digit has been skipped
            if (!skipAllowed && PIN.Count>0) {
                int skippedDigit = checkSkipped(PIN[PIN.Count-1], digit);
                // value found
                if (skippedDigit != 0) {

                    Debug.Log("Inserting skipped value: " + skippedDigit);
                    // only add if digit not already in PIN
                    if(PIN.Contains(skippedDigit)) {
                        PIN.Add(skippedDigit);
                    } 
                    Debug.Log(PIN);
                    // PIN is already complete
                    if (PIN.Count == maxLength) {
                        return;
                    }
                }
            }
            // Debug.Log("Added digit to PIN");
            // only add if digit not already in PIN
            PIN.Add(digit);
            Debug.Log(PIN);
        }
    }

    void resetPIN() {
        PIN.Clear();
        validStart = false;
    }

    private void checkPIN() 
    {
        Debug.Log("checking PIN");

        // check length
        if (PIN.Count != password.Count) {
            Debug.Log("passwords are of different length");
            invalidPassword();
            return;
        }

        // check digits
        for (int i=0; i<PIN.Count; i++) {
            if (password[i] != PIN[i]) {
                invalidPassword();
                return;
            }
        }
        validPassword();
    }

    private void invalidPassword() 
    {
        Debug.Log("Warning. Invalid Password");
        InvalidPasswordEvent.Invoke();
        resetPIN();
    }

    private void validPassword() 
    {
        Debug.Log("Valid Password");
        // open door/ trigger solenoid
        mqtt.PublishTopic(openDoor, "true");
        ValidPasswordEvent.Invoke();
        resetPIN();
    }

    public void SetPassword (List<int> newPassword) 
    {
        password = newPassword;
    }

    private void createSkippableDictionary() 
    {
        Dictionary<int, int> secondDictionary1 = new Dictionary<int, int>();
        secondDictionary1.Add(3, 2);
        secondDictionary1.Add(7, 4);
        secondDictionary1.Add(9, 5);
        Dictionary<int, int> secondDictionary2 = new Dictionary<int, int>();
        secondDictionary2.Add(8, 5);
        Dictionary<int, int> secondDictionary3 = new Dictionary<int, int>();
        secondDictionary3.Add(1, 2);
        secondDictionary3.Add(7, 5);
        secondDictionary3.Add(9, 6);
        Dictionary<int, int> secondDictionary4 = new Dictionary<int, int>();
        secondDictionary4.Add(6, 5);
        Dictionary<int, int> secondDictionary5 = new Dictionary<int, int>();
        Dictionary<int, int> secondDictionary6 = new Dictionary<int, int>();
        secondDictionary6.Add(4, 5);
        Dictionary<int, int> secondDictionary7 = new Dictionary<int, int>();
        secondDictionary7.Add(1, 4);
        secondDictionary7.Add(3, 5);
        secondDictionary7.Add(9, 8);
        Dictionary<int, int> secondDictionary8 = new Dictionary<int, int>();
        secondDictionary8.Add(2, 5);
        Dictionary<int, int> secondDictionary9 = new Dictionary<int, int>();
        secondDictionary9.Add(1, 5);
        secondDictionary9.Add(3, 6);
        secondDictionary9.Add(7, 8);
        skippable_dict.Add(1, secondDictionary1);
        skippable_dict.Add(2, secondDictionary2);
        skippable_dict.Add(3, secondDictionary3);
        skippable_dict.Add(4, secondDictionary4);
        skippable_dict.Add(5, secondDictionary5);
        skippable_dict.Add(6, secondDictionary6);
        skippable_dict.Add(7, secondDictionary7);
        skippable_dict.Add(8, secondDictionary8);
        skippable_dict.Add(9, secondDictionary9);
    }

    private int checkSkipped(int oldDigit, int newDigit) 
    {
        Dictionary<int, int> secondDict = skippable_dict[oldDigit];
        int skippedDigit = secondDict.GetValueOrDefault(newDigit);
        // Debug.Log(oldDigit + ", " + newDigit + ": " + skippedDigit);
        return skippedDigit;
    }

    public bool DigitAlreadySelectedBefore(int digit) {
        return PIN.Contains(digit);
    }


    void runTest1() 
    {
        // should be discarded
        NewDigit(1, 3);
        // start condition check
        NewDigit(1, 5);
        // wrong PIN
        NewDigit(1, 1);
        NewDigit(1, 3);
        NewDigit(1, 3);
        NewDigit(1, 3);
        // start condition check
        NewDigit(1, 5);
        // valid password
        NewDigit(1, 1);
        NewDigit(1, 2);
        NewDigit(1, 3);
        NewDigit(1, 4);

        testDone = true;
    }
}
