using UnityEngine;
using System.Collections;

public class WorldCameraManager : MonoBehaviour {
	public GameObject PlayerCameraAnchor;
	public GameObject PlayerCamera;

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2, Freeze = 3 }
	static RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;    // X-axis sensitivity
	public float sensitivityY = 15F;    // Y-axis sensitivity

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	public float ScrollDistance = 5f;
	public float MinimumDistance = 5f;
	public float MaximumDistance = 15f;
	public float ScrollSpeed = 1f;

	float rotationY = 0F;

	void Update()
	{
		Rotate();
		/*if (Input.GetAxis("Mouse ScrollWheel") != 0f) { */Scroll(Input.GetAxis("Mouse ScrollWheel"));// }
	}

	public static void CursorLock(bool l)
	{
		if (!l)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = l;
			axes = RotationAxes.MouseXAndY;
		}
		else if (l)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = l;
			axes = RotationAxes.Freeze;
		}
	}

	void Rotate()
	{
		if (axes == RotationAxes.MouseXAndY)    // Rotation on x- and y-axes
		{
			float rotationX = PlayerCameraAnchor.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

			PlayerCameraAnchor.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)   // Rotation on x-axis
		{
			PlayerCameraAnchor.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else if (axes == RotationAxes.MouseY)   // Rotation on y-axis
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

			PlayerCameraAnchor.transform.localEulerAngles = new Vector3(-rotationY, PlayerCameraAnchor.transform.localEulerAngles.y, 0);
		}
		else if (axes == RotationAxes.Freeze) { }   // Rotation is frozen, no code necessary
	}

	void Scroll(float amount)
	{
		ScrollDistance = Mathf.Clamp(ScrollDistance + (amount * ScrollSpeed), -MaximumDistance, -MinimumDistance);
		PlayerCamera.transform.localPosition = new Vector3(0f, 0f, ScrollDistance);
	}

	void Start()
	{
		// Make the rigid body not change rotation
		CursorLock(false);
		if (GetComponent<Rigidbody>()) GetComponent<Rigidbody>().freezeRotation = true;
	}

	public void AnchorCamera(GameObject camera, GameObject robot)
	{
		try
		{
			PlayerCameraAnchor.transform.parent = robot.transform;
			PlayerCameraAnchor.transform.localPosition = robot.GetComponent<Rigidbody>().centerOfMass;
		} catch (System.Exception e) { return; }
	}
}
