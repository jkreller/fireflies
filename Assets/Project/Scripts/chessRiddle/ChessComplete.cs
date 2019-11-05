using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessComplete : MonoBehaviour
{
    public bool[] snippetMatches = new bool[24];
    public GameObject fly;
    private Fireflies fireflies;
    float timer;
    public GameObject firstBubble;
    private SphereCollider sphereCollider;
 

    int counter;

    private void Start()
    {
       fireflies = fly.GetComponent<Fireflies>();
        timer = 3;
        sphereCollider = firstBubble.GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 3;

            counter = 0;
            for(int i = 0; i < 24; i++)
            {
                if (snippetMatches[i] == true)
                {
                    counter++;
                }
                if (counter == 24)
                {
                    sphereCollider.enabled = true;
                    fireflies.ActivateAfterInitialize();
                    //Destroy(this.gameObject);
                }
            }
        }



    }
}
