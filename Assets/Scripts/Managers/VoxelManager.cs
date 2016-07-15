using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class VoxelManager : MonoBehaviour {
	public static List<GameObject> voxels = new List<GameObject>();
	public static bool Loaded = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static bool LoadVoxels()
	{
		try
		{
			foreach (GameObject g in Resources.LoadAll<GameObject>("Voxels/" + ((SceneManager.GetActiveScene().name == "Garage") ? "Build" : "Sim")))
			{
				voxels.Add(g);
				Voxel v = g.GetComponent<Voxel>();
				GlobalVars.voxels.Add(v);
			}
			voxels.Sort((a, b) => a.GetComponent<Voxel>().id.CompareTo(b.GetComponent<Voxel>().id));
		} catch (System.Exception e) { return false; }
		return true;
	}

	public static GameObject GetVoxel(int id)
	{
		return voxels.Find((e => e.GetComponent<Voxel>().id == id));
	}
}
