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
    [SerializeField] private UnityEvent onPress = new UnityEvent();
    [SerializeField] private Axis axis = Axis.x;
    [SerializeField] private float pressDistance = 0.05f;

    // Logic fields
    private Vector3 endPoint;
    private Vector3 axisVector = Vector3.right;
    private bool isPressed;
    private bool eventInvoked;
    private bool finishedPressing;

    void Awake()
    {
        endPoint = transform.position;
        switch (axis)
        {
            case Axis.x:
                axisVector = Vector3.right;
                endPoint.x += pressDistance;
                break;
            case Axis.y:
                axisVector = Vector3.up;
                endPoint.y += pressDistance;
                break;
            case Axis.z:
                axisVector = Vector3.forward;
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
        if (Vector3.Distance(transform.position, endPoint) > 0.01f)
        {
            finishedPressing = false;
            transform.position = Vector3.Lerp(transform.position, transform.position + axisVector, 0.01f);
        } else if (!eventInvoked)
        {
            onPress.Invoke();
            eventInvoked = true;
        }
    }

    private void ReleaseButton()
    {
        eventInvoked = false;
        if (Vector3.Distance(transform.position, endPoint) < pressDistance)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position - axisVector, 0.01f);
        } else
        {
            finishedPressing = true;
        }
    }
}
