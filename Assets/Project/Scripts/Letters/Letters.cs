using UnityEngine;
using System.Collections;

public class Letters : MonoBehaviour
{
    // Options
    public int solutionPosition;
    [SerializeField] private float xOffsetLetters = 0.05f;

    // References
    private Rigidbody rb;
    private Controller currentController;
    private Transform room;

    // Logic fields
    private bool snapToField;
    private SolutionField fieldToSnap;
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        room = GameObject.Find("Room").transform;
    }

    void Update()
    {
        if (snapToField && fieldToSnap)
        {
            transform.position = fieldToSnap.transform.position + Vector3.left * xOffsetLetters;
            Vector3 rotation = fieldToSnap.transform.rotation.eulerAngles;
            rotation.y = -rotation.y;
            transform.eulerAngles = rotation;
        }

        //print(Vector3.Distance(transform.position, room.position));
        if (Vector3.Distance(transform.position, room.position) > 10)
        {
            print(startPosition);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SolutionField solutionField = other.GetComponent<SolutionField>();
        if (solutionField && !solutionField.letters)
        {
            snapToField = true;
            solutionField.letters = gameObject;
            fieldToSnap = solutionField;
            currentController?.ReleaseObject();
        }

        if (other.CompareTag("Controller"))
        {
            currentController = other.GetComponent<Controller>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            if (currentController?.objectInHand)
            {
                snapToField = false;
                if (fieldToSnap)
                {
                    fieldToSnap.letters = null;
                    fieldToSnap = null;
                }

                if (!rb.useGravity)
                {
                    rb.useGravity = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            currentController = null;
        }
    }
}
