using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FirefliesOld : MonoBehaviour
{
    // Options
    [SerializeField] private float seperationSpeed = 1;
    [SerializeField] private float seperationDistance = 4;

    // References
    List<Transform> children;

    // Logic fields for seperation
    private List<Vector3> targetPositions;
    private bool doSeperation = false;
    private bool isSeperating = false;

    private void Awake()
    {
        children = transform.GetComponentsInChildren<Transform>().ToList();
        children.Remove(children[0]);
    }

    void Update()
    {
        if (doSeperation)
        {
            targetPositions = children.Select((child, index) =>
            {
                float newX = child.position.x;
                if (index < 2)
                {
                    newX -= seperationDistance;
                } else
                {
                    newX += seperationDistance;
                }
                return new Vector3(newX, child.position.y, child.position.z);
            }).ToList();

            doSeperation = false;
            isSeperating = true;
        }

        if (isSeperating)
        {
            children.ForEach((child) =>
            {
                child.position = Vector3.Lerp(child.position, targetPositions[children.IndexOf(child)], Time.deltaTime * seperationSpeed);
            });
        }
    }

    public void Seperate()
    {
        doSeperation = true;
    }
}
