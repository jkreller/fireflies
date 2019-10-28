﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR;

// Script for handling player input
public class Player : Movable
{
    // Options
    [SerializeField] private float mouseRotationSpeed = 2;

    // References
    private Fireflies firstFireflies;
    private FireFliesInteraction attractBall;

    // Logic fields
    private float rotationX;
    private float rotationY;

    private void Awake()
    {
        firstFireflies = GameObject.Find("Fireflies").GetComponent<Fireflies>();
        attractBall = GameObject.Find("AttractBall")?.GetComponent<FireFliesInteraction>();
    }

    void Update()
    {
        base.Update();
        if (!XRDevice.isPresent)
        {
            // On left mouse click
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    print(hit.transform.tag);
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
                            if (attractBall)
                            {
                                attractBall.moveWithMouse = true;
                            }
                            break;
                    }
                }
            } else if (Input.GetMouseButtonUp(0))
            {
                attractBall.moveWithMouse = false;
            }

            // On moving mouse
            rotationX += mouseRotationSpeed * Input.GetAxis("Mouse X");
            rotationY -= mouseRotationSpeed * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(rotationY, rotationX, 0);

            // Activate fireflies from lamp
            if (Input.GetKey("s"))
            {
                firstFireflies.Activate(transform.position);
            }

            // Follow attract ball
            if (attractBall && Input.GetKey("f"))
            {
                attractBall.shouldFollow = true;
            }
        }
    }
}