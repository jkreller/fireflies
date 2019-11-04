using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBubble : MonoBehaviour
{
    public List<GameObject> bubbles;
    Fireflies fireflies;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fireflies"))
        {
            fireflies = other.gameObject.GetComponent<Fireflies>();
            fireflies.pathFinding(bubbles);

        }

    }
}
