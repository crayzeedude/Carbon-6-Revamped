using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Voxel : MonoBehaviour {
	public int id;
	public string InternalName;
	public string DisplayName;
	public GlobalVars.Category category;

	public bool IsValidConnectionPoint(Vector3 point)
	{
		for (int x = 0; x < transform.childCount; x++)
		{
			if (point.Equals(transform.GetChild(x).localPosition)) return true;
		}
		return false;
	}

	

	public static GameObject LoadVoxel(string name, int mode) { // Mode 0 is build, mode 1 is drive
		string path = (mode == 0) ? ("Voxels/Build/" + name) : ("Voxels/Sim/" + name);
		return Resources.Load<GameObject>(path);
	}
}


