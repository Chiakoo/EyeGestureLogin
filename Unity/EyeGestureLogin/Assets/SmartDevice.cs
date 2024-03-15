using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

public abstract class SmartDevice: MonoBehaviour
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
    public abstract bool IsAvailable();

    public abstract bool Unlock();

    public abstract bool Lock();

    public abstract bool IsUnlocked();

    public abstract int RemainingUnlockTimeInSeconds();

    public abstract ReadOnlyCollection<int> GetPassphrase();
}
