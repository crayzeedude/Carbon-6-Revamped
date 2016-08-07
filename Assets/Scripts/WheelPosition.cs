using UnityEngine;
using System.Collections;

public class WheelPosition : MonoBehaviour {
	public WheelCollider wheelCol;
	public GameObject wheelVis;
	public bool steering;
	public float turnSpeed;     // The speed at which the wheels rotate for steering
	public float maxSteerAngle;
	float angle = 0f;
	RaycastHit hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Physics.Raycast(wheelCol.transform.position, -wheelCol.transform.up, out hit, wheelCol.suspensionDistance + wheelCol.radius))
		{
			wheelVis.transform.position = hit.point + wheelCol.transform.up * wheelCol.radius;
		} else
		{
			wheelVis.transform.position = wheelCol.transform.position - (wheelCol.transform.up * wheelCol.suspensionDistance);
		}

		if (Input.GetKey(KeyCode.W))
		{
			wheelCol.brakeTorque = 0f;
			wheelCol.motorTorque = 2000;
		} else if (Input.GetKey(KeyCode.S))
		{
			wheelCol.brakeTorque = 0f;
			wheelCol.motorTorque = -2000;
		} else
		{
			wheelCol.motorTorque = 0;
			wheelCol.brakeTorque = 2000f;
		}

		if (Input.GetKey(KeyCode.A))
		{
			angle = -maxSteerAngle * Mathf.Sign(transform.localPosition.z) * System.Convert.ToInt32(steering);
			wheelCol.steerAngle = angle;//Mathf.Lerp(wheelCol.steerAngle, angle, turnSpeed);//-20f * Mathf.Sign(transform.localPosition.z);//* Mathf.Sign(transform.localPosition.z) * -Mathf.Sign(transform.localPosition.x);
			wheelVis.transform.localRotation = Quaternion.Slerp(wheelVis.transform.localRotation, Quaternion.Euler(0f, -90f * Mathf.Sign(transform.localPosition.x) + wheelCol.steerAngle, wheelVis.transform.localRotation.z), Time.deltaTime * turnSpeed);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			angle = maxSteerAngle * Mathf.Sign(transform.localPosition.z) * System.Convert.ToInt32(steering);
			wheelCol.steerAngle = angle;//Mathf.Lerp(wheelCol.steerAngle, angle, turnSpeed);//20f * Mathf.Sign(transform.localPosition.z);//Mathf.Sign(transform.localPosition.z) * -Mathf.Sign(transform.localPosition.x);
			wheelVis.transform.localRotation = Quaternion.Slerp(wheelVis.transform.localRotation, Quaternion.Euler(0f, -90f * Mathf.Sign(transform.localPosition.x) + wheelCol.steerAngle, wheelVis.transform.localRotation.z), Time.deltaTime * turnSpeed);
		} else
		{
			angle = 0f;
			wheelCol.steerAngle = angle;//Mathf.Lerp(wheelCol.steerAngle, angle, turnSpeed);//0f;
			wheelVis.transform.localRotation = Quaternion.Slerp(wheelVis.transform.localRotation, Quaternion.Euler(0f, -90f * Mathf.Sign(transform.localPosition.x), wheelVis.transform.localRotation.z), Time.deltaTime * turnSpeed);
		}
	}

	void FixedUpdate()
	{

	}

	public void DebugStuff()
	{
		//Debug.Log(wheelCol.steerAngle);
		Debug.Log(gameObject.name + ": " + wheelCol.rpm.ToString() + " " + wheelCol.sprungMass.ToString() + " " + wheelCol.isGrounded.ToString());
	}
}
