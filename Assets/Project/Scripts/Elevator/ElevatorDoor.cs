using UnityEngine;
using System.Collections;

// Script for moving elevator doors
public class ElevatorDoor : MonoBehaviour
{
    private enum Side { left, right }

    // Options
    [SerializeField] private Side side;
    [SerializeField] private int openingAngle = 80;

    // Logic fields
    private int doorStatus = 0; // -1 = closing; 0 = stays; 1 = opening

    void Update()
    {
        switch (doorStatus)
        {
            case -1:
                // For each side
                switch (side)
                {
                    case Side.left:
                        if (transform.eulerAngles.y < 180)
                        {
                            transform.Rotate(0, 1, 0);
                        }
                        else
                        {
                            doorStatus = 0;
                        }
                        break;
                    case Side.right:
                        if (transform.eulerAngles.y > 180)
                        {
                            transform.Rotate(0, -1, 0);
                        }
                        else
                        {
                            doorStatus = 0;
                        }
                        break;
                }
                break;
            case 1:
                switch (side)
                {
                    // For each side
                    case Side.left:
                        if (transform.eulerAngles.y > 180 - openingAngle)
                        {
                            transform.Rotate(0, -1, 0);
                        }
                        else
                        {
                            doorStatus = 0;
                        }
                        break;
                    case Side.right:
                        if (transform.eulerAngles.y < 180 + openingAngle)
                        {
                            transform.Rotate(0, 1, 0);
                        }
                        else
                        {
                            doorStatus = 0;
                        }
                        break;
                }
                break;
        }
    }

    public void Open()
    {
        doorStatus = 1;
    }

    public void Close()
    {
        doorStatus = -1;
    }
}
