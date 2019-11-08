using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// mouse movement of Labyrinth-Part
public class Puzzle : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDrag()
    {
        rb.AddRelativeForce(new Vector3(-Input.GetAxis("Mouse X") * 10,Input.GetAxis("Mouse Y") * 10,0));
    }

 
}
