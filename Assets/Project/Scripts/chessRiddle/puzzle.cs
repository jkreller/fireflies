using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        //Input.GetAxis("Mouse X") * 2
        //Input.GetAxis("Mouse Y")*2

    }

    void OnMouseDrag()
    {
        Debug.Log("bin drin");
        rb.AddRelativeForce(new Vector3(-Input.GetAxis("Mouse X") * 10,Input.GetAxis("Mouse Y") * 10,0));
      
    }

 
}
