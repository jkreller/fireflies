using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatticeBars : MonoBehaviour
{
    public bool move;
    private float timer = 0.7f;

    void Update()
    {
        if (move)
        {
            if (timer > 0)
            {
                transform.Translate(Vector3.up * Time.deltaTime);
                timer -= Time.deltaTime;
                print(timer);
            }
            if (timer <= 0)
            {
                move = false;
            }
        }
    }

}
