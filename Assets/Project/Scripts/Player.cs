using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for handling player input
public class Player : MonoBehaviour
{
    // Options
    [SerializeField] private float speed = 2;
    [SerializeField] private float movementSpeed = 1;

    // Logic fields
    private float rotationX;
    private float rotationY;
    private bool isMoving;
    private Vector3 targetPosition;
    private MovingCallBack movingCallBack;

    public delegate void MovingCallBack();

    void Update()
    {
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
                        elevator.OpenDoors(this);
                        break;
                }
            }
        }

        // On moving mouse
        rotationX += speed * Input.GetAxis("Mouse X");
        rotationY -= speed * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(rotationY, rotationX, 0);

        // Move player if isset
        if (isMoving)
        {
            Move();
        }
    }

    public void StartMovement(Vector3 targetPos, MovingCallBack movingCallBack = null)
    {
        targetPosition = targetPos;
        isMoving = true;
        this.movingCallBack = movingCallBack;
    }

    private void Move()
    {
        if (Vector3.SqrMagnitude(transform.position - targetPosition) > 0.01)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);
        } else
        {
            isMoving = false;
            if (movingCallBack != null)
            {
                movingCallBack();
                movingCallBack = null;
            }
        }
    }
}