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
    public GameObject electricityBoxDoor;
    public GameObject electricityBoxButton;
    private Button buttonElectricityBox;
    private Button buzzerbuttonScript;
    public GameObject buzzerButton;
    private int chestSolved;
    public GameObject latticebar;
    private LatticeBars latticeBarScript;
    AudioSource audioSource;
    public GameObject lampSound;
  


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
        buttonElectricityBox = electricityBoxButton.GetComponent<Button>();
        buttonElectricityBox.enabled = false;
        buzzerbuttonScript = buzzerButton.GetComponent<Button>();
        buzzerbuttonScript.enabled = false;
        latticeBarScript = latticebar.GetComponent<LatticeBars>();
    }


    void Update()
    {
        if (changeWorld)
        {
            // käfige befüllt
            if (riddleSolvedCount == 1)
            {
                audioSource = lampSound.GetComponent<AudioSource>();
                audioSource.Play();
                chessFireFlies.SetActive(true);

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
                buttonElectricityBox.enabled = true;
                Vector3 to = new Vector3(120, 0, 0);
                electricityBoxDoor.transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
                changeWorld = false;
                audioSource = electricityBoxDoor.GetComponent<AudioSource>();
                audioSource.Play();

            }
         
            // kisten befüllt
            if(riddleSolvedCount == 3)
            {
                buzzerbuttonScript.enabled = true;
                latticeBarScript.move = true;
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

    public void chestSolve()
    {
        
        chestSolved++;
        if (chestSolved == 3)
        {
            riddleSolvedCount = 3;
            changeWorld = true;
        }
    }

    public void labyrinthSolved()
    {
        riddleSolvedCount = 2;
        changeWorld = true;
    }
}
