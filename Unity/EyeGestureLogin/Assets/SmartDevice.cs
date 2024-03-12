using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

public interface SmartDevice
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Returns the availabillity of the device.
    /// </summary>
    /// <returns></returns>
    public bool IsAvailable();

    public bool Unlock();

    public bool Lock();

    public bool IsUnlocked();

    public int RemainingUnlockTimeInSeconds();

    public ReadOnlyCollection<int> GetPassphrase();
}
