using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
	public static GameObject CurrentVoxel;
	public static List<GameObject> voxels = new List<GameObject>();
	
	// Use this for initialization
	void Awake () {
		LoadVoxels();
		Select(0);
		SendMessage("LoadButtons");
		SendMessage("LoadVoxelData");
	}
	
	void LoadVoxels()
	{
		foreach (GameObject g in Resources.LoadAll<GameObject>("Voxels/Build"))
		{
			voxels.Add(g);
			Voxel v = g.GetComponent<Voxel>();
			GlobalVars.voxels.Add(v);
		}
		voxels.Sort((a, b) => a.GetComponent<Voxel>().id.CompareTo(b.GetComponent<Voxel>().id));
	}

	public static void Select(int id)	// Selects a voxel and returns it
	{
		CurrentVoxel = GetVoxel(id);
	}

	public static GameObject GetVoxel(int id)
	{
		return voxels.Find((e => e.GetComponent<Voxel>().id == id));
	}
}
