using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageBubble : MonoBehaviour
{
    private Fireflies fireflies;

    private void OnTriggerStay(Collider other)
    {
            // If is colliding with another fireflies object
            if (other.gameObject.CompareTag("Fireflies"))
        {
                fireflies = other.gameObject.GetComponent<Fireflies>();
                fireflies.flyToNextPoint = true;
            }
    }
}
