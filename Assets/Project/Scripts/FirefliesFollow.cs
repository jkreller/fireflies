using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirefliesFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 0.01f;

	private void Update()
	{
        transform.LookAt(target.position);
        transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
	}

}
