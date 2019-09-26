using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for handling player input
public class Player : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Fireflies firefliesHitted = hit.transform.GetComponent<Fireflies>();
                if (firefliesHitted)
                {
                    firefliesHitted.Seperate(transform.position);
                }
            }
        }
    }
}
