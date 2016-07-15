using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

public class WorldTransitionManager : MonoBehaviour {
	//List<GameObject> voxels = new List<GameObject>();
	public static bool PrepareVoxels()
	{
		/*try
		{
			if (InventoryManager.voxels.Count == 0) return false;
			foreach (GameObject voxel in InventoryManager.voxels)
			{
				voxels.Add(voxel);
			}
		} catch (Exception e) { return false; }*/
		if (VoxelManager.voxels.Count == 0) return false;
		RobotData.Save();
		return true;
	}

	public static void LoadMap()
	{
		SceneManager.LoadScene("MapTest");
	}
}
