using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LockHandler : MonoBehaviour
{

    private GameObject lockOpener;
    private Vector3 initialParentOffset;
    // locked values
    private float lockedHeight = 0.05f;
    private Vector3 lockedPosition;
    // open values
    private float openHeight = 0.07f;
    private Vector3 openPosition;
    private int openYRotation = 180;
    bool lockOpen = false;



    // Start is called before the first frame update
    void Start()
    {
        // go two children down
        lockOpener = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        // Debug.Log("name: " + lockOpener.name);
        initialParentOffset = lockOpener.transform.localPosition;
        lockedPosition = new Vector3 (initialParentOffset.x, lockedHeight, initialParentOffset.z);
        openPosition = new Vector3 (initialParentOffset.x, openHeight, initialParentOffset.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OpenLock() {
        // if lock already open --> return
        if (lockOpen) return;
        Debug.Log("Opening Virtual Lock");
        lockOpener.transform.localPosition = openPosition;
        lockOpener.transform.RotateAround(lockOpener.transform.position, lockOpener.transform.up, openYRotation);
        lockOpen = true;
    }

    public void CloseLock() {
        // if lock already closed --> return
        if (!lockOpen) return;
        Debug.Log("Closing Virtual Lock");
        lockOpener.transform.localPosition = lockedPosition;
        lockOpener.transform.RotateAround(lockOpener.transform.position, lockOpener.transform.up, -openYRotation);
        lockOpen = false;
    }
}
