using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
	public static GameObject CurrentVoxel;
	//public static List<GameObject> voxels = new List<GameObject>();

	public UnityEvent CubeSelectedEvent;
	public static UnityEvent StaticCubeSelectedEvent;
	
	// Use this for initialization
	void Awake () {
		if (SceneManager.GetActiveScene().name == "Garage")
		{
			StaticCubeSelectedEvent = CubeSelectedEvent;
			//SendMessage("LoadVoxelData");
			if (VoxelManager.LoadVoxels())
			{
				Select(0);
				SendMessage("LoadButtons");
				SendMessage("LoadVoxelData");
			}
		}
	}
	
	/*void LoadVoxels()
	{
		foreach (GameObject g in Resources.LoadAll<GameObject>("Voxels/" + ((SceneManager.GetActiveScene().name == "Garage") ? "Build" : "Sim")))
		{
			voxels.Add(g);
			Voxel v = g.GetComponent<Voxel>();
			GlobalVars.voxels.Add(v);
		}
		voxels.Sort((a, b) => a.GetComponent<Voxel>().id.CompareTo(b.GetComponent<Voxel>().id));
	}*/

	public static void Select(int id)	// Selects a voxel and returns it
	{
		CurrentVoxel = VoxelManager.GetVoxel(id);
		StaticCubeSelectedEvent.Invoke();
	}
}
