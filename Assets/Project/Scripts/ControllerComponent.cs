using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

// Parent class for components of the Controllers
public class ControllerComponent : MonoBehaviour
{
    // References
    protected SteamVR_Input_Sources handType;
    protected SteamVR_Behaviour_Pose controllerPose;

    protected void Start()
    {
        controllerPose = GetComponent<SteamVR_Behaviour_Pose>();
        handType = controllerPose.inputSource;
    }
}
