using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Voxel : MonoBehaviour {
	public int id;
	public string InternalName;
	public string DisplayName;
	public GlobalVars.Category category;
	//public List<Vector3> ValidConnectionSides;
	//public Vector3[] ValidConnectionSides;
	public Vector3[] ValidSides;
	public float mass;

	public int RestrictedPlacement; // 0 = none, 1 = wheel
	public int RestrictedRotation; // 0 = none, 1 = wheel, 2 = cube

	void Awake()
	{
		//if (GetComponent<Rigidbody>()) GetComponent<Rigidbody>().mass = mass;
	}

	public bool IsValidConnectionPoint(Vector3 point)
	{
		for (int x = 0; x < transform.childCount; x++)
		{
			if (point.Equals(transform.GetChild(x).localPosition)) return true;
		}
		return false;
	}

	public bool IsValidSide(Voxel tmpname, Vector3 side)
	{
		foreach (Vector3 s in tmpname.ValidSides)
		{
			if (s.Equals(side)) return true;
		}
		return false;
	}

	/*public bool CanPlace(RaycastHit hit)
	{
		//Debug.Log(hit.normal.Round());
		if (GlobalVars.IsInMenu()) return false;
		if (hit.transform.gameObject.layer == 8)
		{
			if (hit.transform.tag == "Voxel" || hit.transform.tag == "Floor")
			{
				if (IsValidConnectionPoint(hit.transform.InverseTransformPoint(hit.point).Round()))
				{
					Debug.Log(IsValidSide(hit.normal.Round()));
					return IsValidSide(hit.normal.Round());
				}
				return false;
			}
			return false;
		}
		return false;
	}*/

	public static GameObject LoadVoxel(string name, int mode) { // Mode 0 is build, mode 1 is drive
		string path = (mode == 0) ? ("Voxels/Build/" + name) : ("Voxels/Sim/" + name);
		return Resources.Load<GameObject>(path);
	}
}


