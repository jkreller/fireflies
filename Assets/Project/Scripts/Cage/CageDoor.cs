using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// open and close cage doors
public class CageDoor : MonoBehaviour
{
    AudioSource audioSource;

    public void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }
    public void closCage()
    {
       
        while (transform.eulerAngles.y >= 1)
        {
            transform.Rotate(0, -5 * Time.deltaTime, 0);
            audioSource.Play();
        }
    }

    public void openCage()
    {
        while (transform.eulerAngles.y <= 90)
        {
            transform.Rotate(0, 5 * Time.deltaTime, 0);
            audioSource.Play();
        }
    }
}
