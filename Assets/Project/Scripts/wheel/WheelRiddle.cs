using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRiddle : MonoBehaviour
{
    // References
    private Elevator elevator;

    // Logic
    public int riddle;
    private bool hasOpenedElevator;

    private void Start()
    {
        elevator = GameObject.Find("Elevator").GetComponentInChildren<Elevator>();
    }

    private void Update()
    {
        if (!hasOpenedElevator && riddle == 3)
        {
            elevator.OpenDoors();
            hasOpenedElevator = true;
        }
    }
}
