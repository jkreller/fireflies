using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snippets : MonoBehaviour
{
    public string objectToCollideWith;
    public int matchNumber;
    SnippetRiddle snippetRiddle;
    private bool inTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        snippetRiddle = GameObject.Find("Schnipsel").GetComponent<SnippetRiddle>();
        
    }

    private void Update()
    {
        if (!inTrigger)
        {
            snippetRiddle.snippetMatches[matchNumber] = false;
        }
        inTrigger = false;
    }

    void OnTriggerStay(Collider other)
    {
        inTrigger = true;
        if(other.gameObject.name == objectToCollideWith){
            snippetRiddle.snippetMatches[matchNumber] = true;
        }
     

    }
}
