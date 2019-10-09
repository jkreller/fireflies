using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Script for handling player input
public class Player : Movable
{
    // References
    private Fireflies firstFireflies;
    private FireFliesAttract fireFliesAttract;

    // Logic fields
    private float rotationX;
    private float rotationY;

    private void Awake()
    {
            //firstFireflies = GameObject.Find("Fireflies").GetComponent<Fireflies>();
            fireFliesAttract = GameObject.Find("AttractBall").GetComponent<FireFliesAttract>();

    }

    new void Update()
    {
        base.Update();

        // On left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                switch (hit.transform.tag)
                {
                    case "Fireflies":
                        Fireflies firefliesHitted = hit.transform.GetComponent<Fireflies>();
                        firefliesHitted.Seperate(transform.position);
                        break;
                    case "Elevator":
                        Elevator elevator = hit.transform.GetComponentInParent<Elevator>();
                        elevator.OpenDoors();
                        break;
                    case "AttractBall":
                        fireFliesAttract.follow = true;
                        Debug.Log("klicken");
                        break;
                       
                }
            }
        }

        // On moving mouse
        rotationX += speed * Input.GetAxis("Mouse X");
        rotationY -= speed * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(rotationY, rotationX, 0);

        if (Input.GetKey("s"))
        {
            firstFireflies.Activate(transform.position);
        }
    }
}