using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LettersChecker : MonoBehaviour
{
    // References
    private SolutionField[] solutionFields;
    private Wheel[] wheels;
    private bool wheelsStopped;

    // Logic
    private bool[] solutionFieldsStatus;

    void Start()
    {
        solutionFields = GetComponentsInChildren<SolutionField>();
        solutionFieldsStatus = new bool[solutionFields.Length];
        wheels = GameObject.Find("FortuneWheels").GetComponentsInChildren<Wheel>();
        foreach (Wheel wheel in wheels)
        {
            wheel.enabled = false;
        }
    }

    void Update()
    {
        int isRightCount = 0;
        foreach (bool isRight in solutionFieldsStatus)
        {
            if (isRight)
            {
                isRightCount++;
            }
        }

        if (!wheelsStopped && isRightCount == solutionFields.Length)
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.enabled = true;
                wheel.stopRoll = false;
            }
            wheelsStopped = true;
        }
    }

    public void AddFieldStatus(int solutionPosition, bool isRight)
    {
        solutionFieldsStatus[solutionPosition] = isRight;
    }
}
