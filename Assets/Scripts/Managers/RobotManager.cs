using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotManager : MonoBehaviour {
	public GameObject PlayerCamera;
	GameObject robot;
	// Use this for initialization
	void Awake()
	{
		robot = new GameObject("Robot");
		if (VoxelManager.LoadVoxels()) { AssembleRobot(); PositionRobot(); }
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void AssembleRobot()
	{
		robot.AddComponent<Rigidbody>();
		robot.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
		List<Vector3> CoMs = new List<Vector3>();
		robot.transform.position = Vector3.zero;
		//robot.AddComponent<Rigidbody>();
		Debug.Log("Loading Robot into world.");
		RobotData.Load();
		Debug.Log("Loading " + RobotData.voxels.Count.ToString() + " voxels into world.");
		foreach (VoxelData v in RobotData.voxels)
		{
			GameObject voxel = Instantiate(VoxelManager.GetVoxel(v.UID.DecodeID()));
			voxel.transform.position = new Vector3(v.position.x, v.position.y, v.position.z);
			voxel.transform.rotation = Quaternion.Euler(v.rotation.x, v.rotation.y, v.rotation.z);
			voxel.transform.parent = robot.transform;
			//voxel.GetComponent<MeshCollider>().convex = true;
			CoMs.Add(v.position.ToVector3());
			robot.GetComponent<Rigidbody>().mass += voxel.GetComponent<Voxel>().mass * 5f;
		}
		robot.GetComponent<Rigidbody>().centerOfMass = CoMs.Average();
		GetComponent<WorldCameraManager>().AnchorCamera(PlayerCamera, robot);
	}

	void PositionRobot() { robot.transform.position = Vector3.up * 10f; }
}
