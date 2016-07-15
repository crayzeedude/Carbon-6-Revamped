using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DataManager : MonoBehaviour {

	void Awake()
	{
		//GameData.Load();
		//Debug.Log(Application.persistentDataPath);
	}

	public void OnSaveButton()
	{
		SaveRobot();
	}

	public void SaveRobot()
	{
		Debug.Log("Collecting voxel data...");
		if (!CollectVoxelData())
		{
			Debug.Log("Collection failed.");
			AbortCollection();
			return;
		}
		Debug.Log("Saving robot data...");
		RobotData.Save();
	}

	public bool CollectVoxelData()	// Returns true if successful
	{
		try
		{
			RobotData.voxels.Clear();
			foreach (GameObject g in new List<GameObject>(GameObject.FindGameObjectsWithTag("Voxel")))
			{
				Vector3 pV = g.transform.position;
				Vector3 rE = g.transform.rotation.eulerAngles;
				VoxelPosition p = new VoxelPosition(pV.x, pV.y, pV.z);
				VoxelRotation r = new VoxelRotation(rE.x, rE.y, rE.z);
				VoxelData v = new VoxelData(g.GetComponent<Voxel>().id, p, r);
				RobotData.voxels.Add(v);
			}
		}
		catch (Exception e)
		{
			Debug.Log("Error collecting voxel data.");
			Debug.Log(e.Message);
			return false;
		}
		return true;
	}

	void AbortCollection()
	{
		RobotData.voxels.Clear();
		RobotData.Load();
	}

	void LoadVoxelData()
	{
		Debug.Log("Loading robot data...");
		RobotData.Load();
		Debug.Log("Loading " + RobotData.voxels.Count.ToString() + " Voxel(s) into Garage.");
		foreach (VoxelData v in RobotData.voxels)
		{
			GameObject voxel = Instantiate(VoxelManager.GetVoxel(v.UID.DecodeID()));
			voxel.transform.position = new Vector3(v.position.x, v.position.y, v.position.z);
			voxel.transform.rotation = Quaternion.Euler(v.rotation.x, v.rotation.y, v.rotation.z);
		}
	}
}
