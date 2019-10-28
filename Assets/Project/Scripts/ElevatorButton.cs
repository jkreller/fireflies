using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for button of elevator
public class ElevatorButton : MonoBehaviour
{
    // Logic fields
    private Elevator elevator;
    private float zEnd;

    void Awake()
    {
        elevator = GameObject.Find("Elevator").GetComponentInChildren<Elevator>();
        zEnd = transform.position.z + 0.05f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            if (transform.position.z < zEnd)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.forward, 0.01f);
            } else
            {
                elevator.OpenDoors();
            }
        }
    }
}
