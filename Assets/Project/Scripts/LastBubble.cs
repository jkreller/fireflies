﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// last bubble trigger functions after getting into object with fireflys
public class LastBubble : MonoBehaviour
{
    private Fireflies firefly;
    public GameObject goal;

    [SerializeField] public UnityEvent onPathComplete = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fireflies"))
        {
            firefly = other.gameObject.GetComponent<Fireflies>();
            onPathComplete.Invoke();
            
        }
    }

    public void activateFirefly()
    {
        firefly.StartMovement(goal.transform.position);
        firefly.deactivateFirefly = false;
    }
}
