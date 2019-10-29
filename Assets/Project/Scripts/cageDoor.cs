using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageDoor : MonoBehaviour
{
    public bool closeCage = false;

    // Update is called once per frame
    void Update()
    {
    if(closeCage){
        if (transform.eulerAngles.y >= 15)
        {
            transform.Rotate(0, -50 * Time.deltaTime, 0);
        }
       }
    }
}
