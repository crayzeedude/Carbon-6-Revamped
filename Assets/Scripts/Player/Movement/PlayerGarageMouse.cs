using UnityEngine;
using System.Collections;

public class PlayerGarageMouse : MonoBehaviour {
	public GameObject Player;

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2, Freeze = 3 }
	static RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;	// X-axis sensitivity
	public float sensitivityY = 15F;	// Y-axis sensitivity

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;

	void Update()
	{
		Rotate();
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
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

			Player.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)   // Rotation on x-axis
		{
			Player.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else if (axes == RotationAxes.MouseY)   // Rotation on y-axis
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

			Player.transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
		else if (axes == RotationAxes.Freeze) { }   // Rotation is frozen, no code necessary
	}

	void Start()
	{
		// Make the rigid body not change rotation
		CursorLock(false);
		if (GetComponent<Rigidbody>()) GetComponent<Rigidbody>().freezeRotation = true;
	}
}
