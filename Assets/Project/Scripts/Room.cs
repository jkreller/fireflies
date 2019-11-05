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
    private Fireflies fireflies;
    public bool changeWorld = false;
    public GameObject chests;
    public GameObject lastBubbleCag1;
    public GameObject lastBubbleCage2;


    void Start()
    {
    riddleSolvedCount = 0;
  
       chessFireFlies.SetActive(false);
        fireflies = chessFireFlies.GetComponent<Fireflies>();
        //chessFireFlies.GetComponent<Animator>().enabled = false;

        chests.SetActive(false);
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
            // käfige befüllt
            if (riddleSolvedCount == 1)
            { 
            chessFireFlies.SetActive(true);
                fireflies.SetAnimState(1);

                foreach (BoxCollider boxCollider in colChildren)
                {
                    boxCollider.enabled = true;
                }
                changeWorld = false;
            }
            // labyrinth gelöst
            if(riddleSolvedCount == 2)
            {
                //sicherungskasten geht auf
                lastBubbleCag1.SetActive(true);
                lastBubbleCage2.SetActive(true);
                chests.SetActive(true);

            }
         
            // kisten befüllt
            if(riddleSolvedCount == 4)
            {
                //blauer knopf kann verwendet werden
                //gitter geht auch
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

    public void labyrinthSolved()
    {
        riddleSolvedCount = 2;
        changeWorld = true;
    }
}
