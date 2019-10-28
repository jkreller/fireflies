using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cageDoor : MonoBehaviour
{
    public bool closeCage = false;

    // Start is called before the first frame update
    void Start()
    {
        //transform.Rotate(0, -90, 0);
    }

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

        //this.gameObject.transform.rotation = Quaternion.RotateTowards(this.gameObject.transform.rotation, new Vector3(0,2.8f,0), 2*Time.deltaTime);

}
