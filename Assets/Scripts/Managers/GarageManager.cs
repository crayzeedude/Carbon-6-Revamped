using UnityEngine;
using System.Collections;

public class GarageManager : MonoBehaviour {
	public GameObject FloorCube;
	public GameObject FloorParent;
	// Use this for initialization
	void Start () {
		InitFloor();
	}

	void InitFloor()
	{
		for (int x = -25; x <= 25; x++)
		{
			for (int z = -25; z <= 25; z++)
			{
				if ((x == 0) && (z == 0)) { } // There's already a floor cube at (0,0,0), we don't need another one
				else {
					GameObject floor = (GameObject)Instantiate(FloorCube, new Vector3(x, 0, z), Quaternion.identity);
					floor.transform.parent = FloorParent.transform;
				}
			}
		}
	}
}
