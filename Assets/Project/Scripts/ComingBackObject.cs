using UnityEngine;
using System.Collections;

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

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        room = GameObject.Find("Room").transform;
    }

    void Update()
    {
        print(Vector3.Distance(transform.position, room.position));
        if (Vector3.Distance(transform.position, room.position) > maxDistanceFromRoom)
        {
            print(startPosition);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }
}
