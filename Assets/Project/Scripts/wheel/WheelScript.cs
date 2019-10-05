using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{
    public bool stoproll;
    private Transform transformOfObject;
    private WheelRiddle wheelRiddle;
    private bool setWheelCorrect = false;

	private void Start()
	{
        transformOfObject = this.gameObject.GetComponent<Transform>();
        wheelRiddle = GameObject.Find("wheels").GetComponent<WheelRiddle>();
	}

	void Update()
    {
        if (!stoproll)
        {
            transformOfObject.Rotate(Vector3.up, -250 * Time.deltaTime);

        }
        if(!stoproll && setWheelCorrect){
            setWheelCorrect = false;
            wheelRiddle.riddle--;
        }
        if(!setWheelCorrect && stoproll && transformOfObject.eulerAngles.x < 58 && transformOfObject.eulerAngles.x > 19 && transformOfObject.eulerAngles.z < 91 && transformOfObject.eulerAngles.z > 89){
            setWheelCorrect = true;
            wheelRiddle.riddle++;
        }
    }
}
