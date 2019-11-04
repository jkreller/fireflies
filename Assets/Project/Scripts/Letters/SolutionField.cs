using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionField : MonoBehaviour
{
    // Options
    public int solutionPosition;

    // References
    private LettersChecker lettersChecker;
    public GameObject letters;

    private void Start()
    {
        lettersChecker = GetComponentInParent<LettersChecker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Letters"))
        {
            Letters letters = other.GetComponent<Letters>();
            lettersChecker.AddFieldStatus(solutionPosition, letters.solutionPosition == solutionPosition);
        }
    }
}
