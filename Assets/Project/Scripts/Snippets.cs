using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class for the small snippets
public class Snippets : MonoBehaviour
{
    public string objectToCollideWith;
    public int matchNumber;
    SnippetRiddle snippetRiddle;
    private bool inTrigger = false;

 
    private void Start()
    {
        snippetRiddle = GameObject.Find("Snippets").GetComponent<SnippetRiddle>();
        
    }

    private void Update()
    {
        if (!inTrigger)
        {
            snippetRiddle.snippetMatches[matchNumber] = false;
        }
        inTrigger = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.isTrigger)
        {
            inTrigger = true;
            if (other.gameObject.name == objectToCollideWith)
            {
                snippetRiddle.snippetMatches[matchNumber] = true;
            }
        }
     

    }
}