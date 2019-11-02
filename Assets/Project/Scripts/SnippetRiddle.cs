using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnippetRiddle : MonoBehaviour
{
    public bool[] snippetMatches = new bool[3];

    // Update is called once per frame
    void Update()
    {
        if(snippetMatches[0] && snippetMatches[1] && snippetMatches[2])
        {
            Debug.Log("rätsel gelöst");
        }
    }
}
