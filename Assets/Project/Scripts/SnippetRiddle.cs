using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnippetRiddle : MonoBehaviour
{
    public bool[] snippetMatches = new bool[3];
    private Fireflies fireflies;
    public GameObject[] combinedSnippets ;

	private void Start()
	{
        fireflies = GameObject.Find("Fireflies").GetComponent<Fireflies>();
	}

	void Update()
    {
        if(snippetMatches[0] && snippetMatches[1] && snippetMatches[2])
        {

            combinedSnippets = GameObject.FindGameObjectsWithTag("snippet");

            foreach (GameObject snippet in combinedSnippets)
            {
                snippet.GetComponent<MeshRenderer>().enabled = true;
            }

            fireflies.ActivateAfterInitialize();
             
            Destroy(this.gameObject);
        }
    }

}