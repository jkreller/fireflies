using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirefliesFollow : MonoBehaviour
{
    public Transform target;
    float speed = 2f;
    const float EPSILON = 0.1f;

	private void Update()
	{
        transform.LookAt(target.position);
        transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
	}

}
