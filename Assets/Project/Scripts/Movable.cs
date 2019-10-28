using UnityEngine;
using System.Collections;

// Script for movable objects
public class Movable : MonoBehaviour
{
    // Options
    [Header("Movements")]
    [SerializeField] protected float movementSpeed = 1;

    // Logic fields
    protected bool isMoving;
    protected Vector3 targetPosition;
    protected MovingCallBack movingCallBack;

    public delegate void MovingCallBack();

    protected void Update()
    {
        // Move object if isset
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

    protected void Move()
    {
        if (Vector3.SqrMagnitude(transform.position - targetPosition) > 0.01)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);
        }
        else
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
