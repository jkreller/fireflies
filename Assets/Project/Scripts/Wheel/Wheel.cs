using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool stopRoll;
    private Transform transformOfObject;
    private WheelRiddle wheelRiddle;
    private bool setWheelCorrect = false;

	private void Start()
	{
        transformOfObject = this.gameObject.GetComponent<Transform>();
        wheelRiddle = GameObject.Find("FortuneWheels").GetComponent<WheelRiddle>();
	}

	void Update()
    {
        if (!stopRoll)
        {
            transformOfObject.Rotate(Vector3.up, -250 * Time.deltaTime);
        }
        if(!stopRoll && setWheelCorrect){
            setWheelCorrect = false;
            wheelRiddle.riddle--;
        }
        if(!setWheelCorrect && stopRoll && transformOfObject.eulerAngles.x < 58 && transformOfObject.eulerAngles.x > 19 && transformOfObject.eulerAngles.z < 91 && transformOfObject.eulerAngles.z > 89){
            setWheelCorrect = true;
            wheelRiddle.riddle++;
        }
    }

    public void ToggleRoll()
    {
        stopRoll = !stopRoll;
    }
}
