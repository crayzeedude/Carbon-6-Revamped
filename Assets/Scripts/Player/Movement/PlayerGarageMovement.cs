using UnityEngine;
using System.Collections;

public class PlayerGarageMovement : MonoBehaviour {

	// The speed at which we move
	public float MovementSpeed;

	// The player GameObject
	public GameObject Player;

	// Update is called once per frame
	void Update () {
		//Move();
		if (Player.GetComponent<Rigidbody>()) Player.GetComponent<Rigidbody>().velocity = Vector3.zero;	// prevent rigidbody from messing up camera

		// Get input and move the player in the corresponding direction
		Rigidbody tmp = Player.GetComponent<Rigidbody>();

		if (!GlobalVars.IsInMenu())
		{
			// Forward, backward, left and right movement
			if (Input.GetKey(KeyCode.W)) { Player.transform.Translate(GetMovementVector("Forward", MovementSpeed)); }
			if (Input.GetKey(KeyCode.S)) { Player.transform.Translate(GetMovementVector("Backward", MovementSpeed)); }
			if (Input.GetKey(KeyCode.A)) { Player.transform.Translate(GetMovementVector("Left", MovementSpeed)); ; }
			if (Input.GetKey(KeyCode.D)) { Player.transform.Translate(GetMovementVector("Right", MovementSpeed)); }

			// Upwards and downwards movement
			if (Input.GetKey(KeyCode.Space)) { Player.transform.Translate(GetMovementVector("Up", MovementSpeed / 2), Space.World); }
			if (Input.GetKey(KeyCode.LeftShift)) { Player.transform.Translate(GetMovementVector("Down", MovementSpeed / 2), Space.World); }
		}

	}
	static Vector3 GetMovementVector(string Direction, float Speed) {
		/*
		 * <summary>
		 * Move the GameObject in the corresponding direction that
		 * is returned by the string name 'Direction'
		 * </summary>
		 */

		Direction = Direction.ToUpper();

		if (Direction == "FORWARD") {return Vector3.forward * Speed * Time.deltaTime;}
		if (Direction == "BACKWARD") {return Vector3.back * Speed * Time.deltaTime;}
		if (Direction == "LEFT") {return Vector3.left * Speed * Time.deltaTime;}
		if (Direction == "RIGHT") {return Vector3.right * Speed * Time.deltaTime;}
		if (Direction == "UP") {return Vector3.up * Speed * Time.deltaTime;}
		if (Direction == "DOWN") {return Vector3.down * Speed * Time.deltaTime;}

		//Return null if no code paths are found, Or if the user hasnt typed in a correct Direction
		Debug.Log("Could not return a suitable direction. Check spelling and confirm that you are entering it correctly.");
		return Vector3.zero;
	}

	void Move()		// no idea how to get this working so i'm gonna leave it here just in case
	{
		Rigidbody tmp = Player.GetComponent<Rigidbody>();
		// Forward, backward, left and right movement
		if (Input.GetKey(KeyCode.W))
		{
			Player.GetComponent<Rigidbody>().velocity = new Vector3(GetMovementVector("Forward", MovementSpeed).x, tmp.velocity.y, tmp.velocity.z);
		}
		else if (Input.GetKey(KeyCode.S)) {
			Player.GetComponent<Rigidbody>().velocity = new Vector3(GetMovementVector("Backward", MovementSpeed).x, tmp.velocity.y, tmp.velocity.z);
		}
		else Player.GetComponent<Rigidbody>().velocity = new Vector3(0f, tmp.velocity.y, tmp.velocity.z);

		if (Input.GetKey(KeyCode.A)) {
			Player.GetComponent<Rigidbody>().velocity = new Vector3(tmp.velocity.x, tmp.velocity.y, GetMovementVector("Left", MovementSpeed).z);
		}
		else if (Input.GetKey(KeyCode.D)) {
			Player.GetComponent<Rigidbody>().velocity = new Vector3(tmp.velocity.x, tmp.velocity.y, GetMovementVector("Right", MovementSpeed).z);
		}
		else Player.GetComponent<Rigidbody>().velocity = new Vector3(tmp.velocity.x, tmp.velocity.y, 0f);

		// Upwards and downwards movement
		if (Input.GetKey(KeyCode.Space)) {
			Player.GetComponent<Rigidbody>().velocity = new Vector3(tmp.velocity.x, GetMovementVector("Up", MovementSpeed).y, tmp.velocity.z);
		}
		else if (Input.GetKey(KeyCode.LeftShift)) {
			Player.GetComponent<Rigidbody>().velocity = new Vector3(tmp.velocity.x, GetMovementVector("Down", MovementSpeed).y, tmp.velocity.z);
		}
		else Player.GetComponent<Rigidbody>().velocity = new Vector3(tmp.velocity.x, 0f, tmp.velocity.z);
	}
}
