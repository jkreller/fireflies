using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Script for button which is pressable by VR-controllers and mouse
public class Button : MonoBehaviour
{
    private enum Axis
    {
        x, y, z
    }

    // Options
    [SerializeField] private UnityEvent onFirstPress = new UnityEvent();
    [SerializeField] private UnityEvent onSecondPress = new UnityEvent();
    [SerializeField] private Axis axis = Axis.x;
    [SerializeField] private float pressDistance = 0.05f;

    // Logic fields
    private Vector3 endPoint;
    private Vector3 axisVector = Vector3.right;
    private bool isPressed;
    private bool eventInvoked;
    private bool finishedPressing;
    private bool isFirstPress = true;

    private void Awake()
    {
        endPoint = transform.position;
        switch (axis)
        {
            case Axis.x:
                axisVector = Vector3.right * Mathf.Sign(pressDistance);
                endPoint.x += pressDistance;
                break;
            case Axis.y:
                axisVector = Vector3.up * Mathf.Sign(pressDistance);
                endPoint.y += pressDistance;
                break;
            case Axis.z:
                axisVector = Vector3.forward * Mathf.Sign(pressDistance);
                endPoint.z += pressDistance;
                break;
        }
    }

    private void Update()
    {
        if (isPressed)
        {
            PressButton();
        } else if (!finishedPressing)
        {
            ReleaseButton();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            isPressed = false;
        }
    }

    private void OnMouseDown()
    {
        isPressed = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
    }

    private void PressButton()
    {
        // If didn't reach end
        if (Vector3.Distance(transform.position, endPoint) > 0.01f)
        {
            finishedPressing = false;
            // Move button towards end
            transform.position = Vector3.Lerp(transform.position, transform.position + axisVector, 0.01f);
        } else if (!eventInvoked) // When didn't called event yet
        {
            // If is first press
            if (isFirstPress)
            {
                onFirstPress.Invoke();

                // If second press is available then call next time second press
                if (onSecondPress.GetPersistentEventCount() > 0)
                {
                    isFirstPress = false;
                }
            } else
            {
                onSecondPress.Invoke();
                isFirstPress = true;
            }
            eventInvoked = true;
        }
    }

    private void ReleaseButton()
    {
        eventInvoked = false;
        // If didn't reach beginning again
        if (Vector3.Distance(transform.position, endPoint) < pressDistance)
        {
            // Move button back to beginning
            transform.position = Vector3.Lerp(transform.position, transform.position - axisVector, 0.01f);
        } else
        {
            finishedPressing = true;
        }
    }
}
