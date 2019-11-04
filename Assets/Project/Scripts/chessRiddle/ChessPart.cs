using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPart : MonoBehaviour
{
    public GameObject puzzlePart;
    bool inTrigger;
    ChessComplete chessComplete;
    public int position;

    void Start()
    {
        chessComplete = GameObject.Find("Labyrinth").GetComponent<ChessComplete>();
    }
    private void Update()
    {
        if (!inTrigger)
        {
            chessComplete.snippetMatches[position] = false;
        }
        inTrigger = false;
    }

    void OnTriggerStay(Collider other)
    {
        inTrigger = true;
        if (other.gameObject.name == puzzlePart.gameObject.name)
        {
            chessComplete.snippetMatches[position] = true;
       
        }


    }
}
