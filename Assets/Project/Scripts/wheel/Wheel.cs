using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [System.NonSerialized] public bool stopRoll = true;
    private WheelRiddle wheelRiddle;
    private bool setWheelCorrect = false;

	private void Start()
	{
        wheelRiddle = GameObject.Find("FortuneWheels").GetComponent<WheelRiddle>();
	}

	void Update()
    {
        if (!stopRoll)
        {
            transform.Rotate(Vector3.up, -100 * Time.deltaTime);
        }
        if(!stopRoll && setWheelCorrect){
            setWheelCorrect = false;
            wheelRiddle.riddle--;
        }
        if(!setWheelCorrect && stopRoll && transform.eulerAngles.x < 58 && transform.eulerAngles.x > 19 && transform.eulerAngles.z < 91 && transform.eulerAngles.z > 89){
            setWheelCorrect = true;
            wheelRiddle.riddle++;
        }
    }

    public void ToggleRoll()
    {
        stopRoll = !stopRoll;
    }
}
