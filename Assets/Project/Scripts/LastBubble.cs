using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LastBubble : MonoBehaviour
{

    [SerializeField] public UnityEvent onPathComplete = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fireflies"))
        {

            onPathComplete.Invoke();
        }
    }


}
