using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableHandler : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(target.position);
    }
}
