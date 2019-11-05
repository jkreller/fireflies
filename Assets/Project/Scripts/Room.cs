using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject chessFireFlies;
    public GameObject chessRiddle;
    public int riddleSolvedCount;
    private int cageSolved = 0;
    private BoxCollider[] colChildren;
    bool changeWorld = false;


    void Start()
    {

        changeWorld = true;
        riddleSolvedCount = 1;
  
        //chessFireFlies.SetActive(false);

        colChildren = chessRiddle.GetComponentsInChildren<BoxCollider>();
        foreach(BoxCollider boxCollider in colChildren)
        {
            boxCollider.enabled = false;
        }

    }


    void Update()
    {
        if (changeWorld)
        {
            if (riddleSolvedCount == 1)
            {
                chessFireFlies.SetActive(true);
                foreach (BoxCollider boxCollider in colChildren)
                {
                    boxCollider.enabled = true;
                }
                changeWorld = false;
            }
        }
    }

    public void cageSolve()
    {
        cageSolved++;
        if(cageSolved == 2)
        {
            riddleSolvedCount = 1;
            changeWorld = true;
        }
    }
}
