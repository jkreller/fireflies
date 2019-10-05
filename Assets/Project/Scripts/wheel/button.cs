using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public GameObject wheel;
    private WheelScript wheelScript;

    void Start()
    {
        wheelScript = wheel.GetComponent<WheelScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        if (wheelScript.stoproll)
        {
            wheelScript.stoproll = false;
        }
        else
        {
            wheelScript.stoproll = true;
        }
    } 
}
