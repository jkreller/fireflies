using UnityEngine;
using System.Collections;

// Script for each letters object
public class Letters : MonoBehaviour
{
    // Options
    public int solutionPosition;
    [SerializeField] private float xOffsetLetters = 0.05f;

    // References
    private Rigidbody rb;
    private Controller currentController;

    // Logic fields
    private bool snapToField;
    private SolutionField fieldToSnap;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (snapToField && fieldToSnap)
        {
            // Snap to solution field
            transform.position = fieldToSnap.transform.position + Vector3.left * xOffsetLetters;
            Vector3 rotation = fieldToSnap.transform.rotation.eulerAngles;
            rotation.y = -rotation.y;
            transform.eulerAngles = rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If solution field is touched
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
