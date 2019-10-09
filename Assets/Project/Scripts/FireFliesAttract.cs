using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFliesAttract : MonoBehaviour
{
    public bool follow;
    private Vector3 mOffset;
    private float mZCoord;

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void Update()
    {
        if(follow){
            Vector3 temp = Input.mousePosition;
            temp.z = Input.mousePosition.z+1.5f; 
            this.transform.position = Camera.main.ScreenToWorldPoint(temp);
        }
        if(Input.GetMouseButtonUp(0)){
            follow = false;
        }
    }
}