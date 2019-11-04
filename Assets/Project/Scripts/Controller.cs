using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

// Script for the controllers of the player - Reference: https://www.raywenderlich.com/9189-htc-vive-tutorial-for-unity
public class Controller : ControllerComponent
{
    // References
    [SerializeField] private SteamVR_Action_Boolean grabGripAction;

    // Logic fields
    [System.NonSerialized] public GameObject objectInHand;
    private GameObject collidingObject;
    private Vector3? dragStartPosition;

    void Update()
    {
        if (grabGripAction.GetLastStateDown(handType))
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }

        if (grabGripAction.GetLastStateUp(handType) && objectInHand)
        {
            ReleaseObject();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (collidingObject)
        {
            collidingObject = null;
        }
    }

    private void SetCollidingObject(Collider col)
    {
        // Check if no colliding object is set, object has rigidbody and is in layer "Grabbing"
        if (!collidingObject && col.GetComponent<Rigidbody>() && col.gameObject.layer == 10)
        {
            collidingObject = col.gameObject;
        }
    }

    private void GrabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;
        FixedJoint joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fj = gameObject.AddComponent<FixedJoint>();
        fj.breakForce = 20000;
        fj.breakTorque = 20000;
        return fj;
    }

    public void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            objectInHand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
            objectInHand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();
        }
        objectInHand = null;
    }
}
