using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class EyePatternHandler : MonoBehaviour
{
    private bool EyePatternEnabled = false; 
    // Start is called before the first frame update
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
            for(int i = 0;  i < transform.childCount; i++){
                GameObject digit = transform.GetChild(i).gameObject;
                digit.GetComponent<SingleGazePoint>().deviceId = id;
                digit.SetActive(true);
            }
        }
    }

    public void Disable(){
        if(EyePatternEnabled){
            for(int i = 0;  i < transform.childCount; i++){
                GameObject digit = transform.GetChild(i).gameObject;
                digit.SetActive(false);
            }
        }
    }
}
