using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockCalibration : MonoBehaviour
{

    public InputActionProperty button;
    [SerializeField]
    private GameObject calibrationController;
    private float currAppliedRotationY = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (button.action.ReadValue<bool>()) {
            Debug.Log("button triggered");
            calibrateLock();
        }
    }

    // rotate only works once...
    void calibrateLock() {
        // set smartDevice to current controller position
        this.gameObject.transform.position = calibrationController.transform.position;
        
        // undo earlier rotation
        this.transform.Rotate(0, -currAppliedRotationY, 0);
        // rotate smartDevice to current controller y rotation
        currAppliedRotationY = calibrationController.transform.eulerAngles.y;
        // this.transform.Rotate(this.transform.eulerAngles.x, yRotation, this.transform.eulerAngles.z);
        this.transform.Rotate(0, currAppliedRotationY, 0);

        // rotate around axis
        // which point??
        // this.transform.RotateAround(this.gameObject.pivot)


    }
}
