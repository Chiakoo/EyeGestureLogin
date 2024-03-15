using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections.ObjectModel;

public class Validator : MonoBehaviour
{
    [Header("Validator Settings")]
    [Tooltip("true: 1,3 valid. false: 1,3 becomes 1,2,3")]
    public bool skipAllowed = false;
    [Tooltip("seconds without input until timeout triggered/ password entry ends")]
    [Range(1, 60)]
    public int timeout = 5;
    [Tooltip("maximum password length possible (all fields)")]
    public int maxLength = 9;

    [Header("Unity Events")]
    [SerializeField]
    private UnityEvent DeviceNotConnected = new UnityEvent();
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
    private  ReadOnlyCollection<int> password = (new List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9}).AsReadOnly();
    // the so far entered PIN
    private List<int> PIN = new List<int>();
    // is test already done?
    private bool testDone = false;
    private int lastTimestamp = 0;

    SmartConnector smartConnector;
    SmartDevice smartDevice;
    

    // Start is called before the first frame update
    void Start()
    {
        smartConnector = GameObject.Find("SmartConnector").GetComponent<SmartConnector>();

        createSkippableDictionary();
    }


    int digit = 1;
    void Update()
    {
        // // Testing
        // if (!testDone) {
        //     // runTest1();
        //     NewDigit(0, 5);
        //     testDone = true;
        // }
        // NewDigit(0, digit);
        // digit ++;
        
        // only check if someone is currently entering PIN
        if (PIN.Count<=0) return;

        // timeout
        if (DateTime.Now.Second > lastTimestamp + timeout) {
            Debug.Log("password timeout");
            invalidPassword();
        }
        // reached maximum length
        else if (PIN.Count >= maxLength) {
            Debug.Log("max Length reached");
            invalidPassword();
        }
    }


    public void NewDigit(int ID, int digit) 
    {
        // trying to access different device
        if (currDeviceID != ID) {
            resetPIN();
            changeDevice(ID);
        }
        
        // if digit already in PIN -> return
        if(PIN.Contains(digit)) return;
        
        // if skipping is not allowed, check if digit has been skipped
        if (!skipAllowed && PIN.Count>0) {
            int skippedDigit = checkSkipped(PIN[PIN.Count-1], digit);
            // value found
            if (skippedDigit != 0) {

                // only add if skipped digit not already in PIN
                if(!PIN.Contains(skippedDigit)) {
                    Debug.Log("added skipped digit: " + skippedDigit);
                    PIN.Add(skippedDigit);
                    checkPIN();
                    lastTimestamp = DateTime.Now.Second;
                }
                // trying to enter invalid scheme
                // skipped digit already selected --> not allowed
                else {
                    invalidPassword();
                } 
                // PIN is already complete
                if (PIN.Count == maxLength) {
                    return;
                }
            }
        }
        // no digit was skipped --> add
            Debug.Log("added: " + digit);
            PIN.Add(digit);
            checkPIN();
            lastTimestamp = DateTime.Now.Second;
    }

    void changeDevice(int ID) 
    {
        currDeviceID = ID;
        // get password of new device from SmartConnector 
        smartDevice = smartConnector.GetSmartDevice(currDeviceID);
        password = smartDevice.GetPassphrase();
        Debug.Log("new device: " +  currDeviceID);
        Debug.Log("new password: " +  password.ToString());
}

    void resetPIN() {
        PIN.Clear();
    }

    // always called when adding digit
    private void checkPIN() 
    {
        Debug.Log("checking PIN");

        if (PIN.Count != password.Count) return;

        // check digits
        for (int i=0; i<PIN.Count; i++) {
            if (password[i] != PIN[i]) {
                Debug.Log(PIN[i] + " did not match " +  password[i]);
                return;
            }
        }
        validPassword();
    }
    private void validPassword() 
    {
        Debug.Log("Valid Password");
        // check if device is connected
        if (!smartDevice.IsAvailable()) {
            DeviceNotConnected.Invoke();
            Debug.LogWarning("Current device is not connected");
        } 
        // tell UI to show success        
        ValidPasswordEvent.Invoke();
        // open door/ trigger solenoid
        smartDevice.Unlock();
        resetPIN();
    }

    private void invalidPassword() 
    {
        Debug.LogWarning("Warning. Invalid Password");
        InvalidPasswordEvent.Invoke();
        resetPIN();
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
        // valid PIN
        NewDigit(1, 1);
        NewDigit(1, 2);
        NewDigit(1, 3);
        NewDigit(1, 4);

        testDone = true;
    }
}
