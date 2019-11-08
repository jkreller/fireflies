using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

// Script for the laser pointer of the player - Reference: https://www.raywenderlich.com/9189-htc-vive-tutorial-for-unity
public class LaserPointer : ControllerComponent
{
    // Options
    [SerializeField] private Vector3 teleportPointerOffset;
    [SerializeField] private LayerMask teleportMask;

    // References
    [SerializeField] private SteamVR_Action_Boolean teleportAction;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject teleportReticlePrefab;
    private GameObject laser;
    private GameObject pointer;
    private Transform teleportPointerTransform;
    private Transform cameraRigTransform;
    private Transform headTransform;

    // Logic fields
    private bool shouldTeleport;
    private Vector3 hitPoint;

    new void Start()
    {
        base.Start();
        laser = Instantiate(laserPrefab);
        pointer = Instantiate(teleportReticlePrefab);
        teleportPointerTransform = pointer.transform;
        cameraRigTransform = GameObject.Find("Player").transform;
        headTransform = cameraRigTransform.Find("Camera").transform;
    }

    private void Update()
    {
        if (teleportAction.GetState(handType))
        {
            RaycastHit hit;
            // If teleportation ray hits layer
            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100, teleportMask))
            {
                hitPoint = hit.point;
                ShowLaser(hit);
                pointer.SetActive(true);
                teleportPointerTransform.position = hitPoint + teleportPointerOffset;
                shouldTeleport = true;
            } else
            {
                HideLaser();
            }
        } else
        {
            HideLaser();
        }

        if (teleportAction.GetStateUp(handType))
        {
            HideLaser();
            if (shouldTeleport)
            {
                Teleport();
            }
        }
    }

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laser.transform.position = transform.position + (hitPoint - transform.position) / 2;
        laser.transform.LookAt(hitPoint);
        laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y, hit.distance);
    }

    private void HideLaser()
    {
        laser.SetActive(false);
        pointer.SetActive(false);
    }

    private void Teleport()
    {
        shouldTeleport = false;
        pointer.SetActive(false);
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        difference.y = 0;
        cameraRigTransform.position = hitPoint + difference;
    }
}
