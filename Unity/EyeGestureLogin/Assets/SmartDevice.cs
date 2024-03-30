using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Class <c>SmartDevice</c> is an abstract class (similar to interface) to define the functions that must be implemented by the specific smart device.
/// </summary>
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

    /// <summary>
    /// Unlocks the smart device.
    /// </summary>
    /// <returns></returns>
    public abstract bool Unlock();

    /// <summary>
    /// Locks the smart device.
    /// </summary>
    /// <returns></returns>
    public abstract bool Lock();

    /// <summary>
    /// Checks wether the smart device is unlocked.
    /// </summary>
    /// <returns></returns>
    public abstract bool IsUnlocked();

    /// <summary>
    /// Every smart device has an countdown until it turns back into the locked state after it god unlocked.
    /// </summary>
    /// <returns></returns>
    public abstract int RemainingUnlockTimeInSeconds();

    /// <summary>
    /// The passcode of the smart device not changable.
    /// </summary>
    /// <returns></returns>
    public abstract ReadOnlyCollection<int> GetPassphrase();
}
