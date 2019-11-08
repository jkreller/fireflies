using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for solution fields of letter puzzle
public class SolutionField : MonoBehaviour
{
    // Options
    public int solutionPosition;

    // References
    private LettersChecker lettersChecker;
    [System.NonSerialized] public GameObject letters;

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
