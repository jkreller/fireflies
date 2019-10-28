using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble : MonoBehaviour
{
    private Fireflies fireflies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
