using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class FireFliesAttract : ControllerComponent
{
    // References
    private Fireflies fireflies;
    [SerializeField] private SteamVR_Action_Boolean grabPinchAction;

    // Logic fields
    [System.NonSerialized] public bool moveWithMouse;
    [System.NonSerialized] public bool shouldFollow;

    private void Awake()
    {
        fireflies = GameObject.Find("Fireflies").GetComponent<Fireflies>();
    }

    void Update()
    {
        if (XRDevice.isPresent)
        {
            if (grabPinchAction.GetStateDown(handType))
            {
                shouldFollow = true;
            } else if (grabPinchAction.GetLastStateUp(handType))
            {
                shouldFollow = false;
            }
        } else if (moveWithMouse)
        {
            Vector3 temp = Input.mousePosition;
            temp.z = Input.mousePosition.z + 1.5f;
            this.transform.position = Camera.main.ScreenToWorldPoint(temp);
        } else
        {
            shouldFollow = false;
        }
    }
}