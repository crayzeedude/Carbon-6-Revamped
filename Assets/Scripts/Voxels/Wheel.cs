using UnityEngine;
using System.Collections;

public class Wheel : MonoBehaviour {
	public float MotorDrive;
	public float MaxMotorDrive;
	public AnimationCurve Acceleration;
	public float drive = 0.5f;
	// Use this for initialization
	void Start () {
		Acceleration.AddKey(0f, -MaxMotorDrive);
		Acceleration.AddKey(0.5f, 0f);
		Acceleration.AddKey(1f, MaxMotorDrive);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && drive < 1f)
		{
			drive += 0.02f;
		} else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
		{
			drive = 0.5f;
		} else if (!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) && drive > 0f)
		{
			drive -= 0.02f;
		}

		MotorDrive = ((Acceleration.Evaluate(drive) - 0.5f) * 2f) * MaxMotorDrive;
		GetComponent<WheelCollider>().motorTorque = MotorDrive;
	}
}
