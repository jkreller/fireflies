using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageDoor : MonoBehaviour
{

    public void closCage()
    {
       
        while (transform.eulerAngles.y >= 12)
        {
            transform.Rotate(0, -50 * Time.deltaTime, 0);
        }
    }

    public void openCage()
    {
        while (transform.eulerAngles.y <= 90)
        {
            print(transform.eulerAngles.y);
            transform.Rotate(0, 50 * Time.deltaTime, 0);
        }
    }
}
