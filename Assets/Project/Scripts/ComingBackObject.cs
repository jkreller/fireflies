using UnityEngine;
using System.Collections;

// Script for objects who are grabbable and should come back to room
public class ComingBackObject : MonoBehaviour
{
    // Options
    [SerializeField] private float maxDistanceFromRoom = 10;

    // References
    private Rigidbody rb;
    private Transform room;

    // Logic
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
        room = GameObject.Find("Room").transform;
    }

    private void Update()
    {
        // If is too far away from room
        if (Vector3.Distance(transform.position, room.position) > maxDistanceFromRoom)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            if (tag == "Letters")
            {
                rb.useGravity = false;
            }
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }
}
