using UnityEngine;
using System.Collections;
using UnityEngine.XR;
using Valve.VR;

public class LabyrinthInteraction : ControllerComponent
{
    // References
    [SerializeField] private SteamVR_Action_Boolean grabGripAction;

    // Logic fields
    private Rigidbody collidingObject;


    private void Update()
    {
        if (XRDevice.isPresent)
        {
            if (grabGripAction.GetLastStateDown(handType))
            {
                if (collidingObject)
                {
                    collidingObject = null;
                }
            }

            if (grabGripAction.GetLastStateUp(handType))
            {
                ResetCollidingObject();
            }

            if (grabGripAction.GetState(handType) && collidingObject != null)
            {
                collidingObject.velocity = controllerPose.GetVelocity();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    private void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        ResetCollidingObject();
    }

    private void SetCollidingObject(Collider other)
    {
        if (collidingObject == null && other.CompareTag("LabyrinthPart"))
        {
            collidingObject = other.GetComponent<Rigidbody>();
        }
    }

    private void ResetCollidingObject()
    {
        if (collidingObject)
        {
            collidingObject.velocity *= 0.5f;
            collidingObject = null;
        }
    }
}
