using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessComplete : MonoBehaviour
{
    public bool[] snippetMatches = new bool[24];
    private Fireflies fireflies;
    float timer;

    int counter;

    private void Start()
    {
        timer = 2;
    }

    void Update()
    {

         
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 2;

            counter = 0;
            for(int i = 0; i < 24; i++)
            {
                if (snippetMatches[i] == true)
                {
                    counter++;
                }
                if (counter == 24)
                {
                    Debug.Log("zfu");
                }
            }
        }



    }
}
