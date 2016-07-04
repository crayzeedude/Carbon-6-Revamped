using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGarage : MonoBehaviour {
	public Vector3 hitNm;

	// Build distance
	public float buildDist;

	public GameObject ghost;

	int rotIndex = 0;
	Vector3 angles = Vector3.zero;

	public LayerMask layersToRaycast;

	//The data that the raycast hit.
	RaycastHit hit; 

	void Start()
	{
		
	}

	// Update is called once per frame
	void Update () {
		ghost.GetComponent<MeshFilter>().mesh = InventoryManager.CurrentVoxel.GetComponent<MeshFilter>().sharedMesh;
		ghost.GetComponent<MeshCollider>().sharedMesh = InventoryManager.CurrentVoxel.GetComponent<MeshCollider>().sharedMesh;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)), out hit, buildDist, layersToRaycast))
		{
			hitNm = hit.normal.Round();
			if (CanPlace()) {
				ghost.transform.position = CubePosition();
				ghost.transform.rotation = CubeRotation();
				if (Input.GetMouseButtonDown(0))
				{
					GameObject voxel = (GameObject)Instantiate(InventoryManager.CurrentVoxel, CubePosition(), ghost.transform.rotation);//InventoryManager.CurrentVoxel.transform.rotation.SnapToRightAngle());
				}
			} else { ghost.transform.position = new Vector3(0, -500, 0); }
			if (CanDestroy())
			{
				if (Input.GetMouseButtonDown(2))
				{
					InventoryManager.Select(hit.transform.gameObject.GetComponent<Voxel>().id);
				}
				else if (Input.GetMouseButtonDown(1))
				{
					Destroy(hit.transform.gameObject);
				}
			}
		}
		else { ghost.transform.position = new Vector3(0, -500, 0); }
		if (!GlobalVars.IsInMenu() && (Input.GetAxis("Mouse ScrollWheel") != 0))
		{
			RotIndex((int)Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")));
		}
	}

	Vector3 CubePosition()
	{
		//Debug.Log(hit.normal);
		if (CanPlace())
		{
			return (hit.transform.position + hitNm).Round();
		}
		else return new Vector3(0, -500, 0);
	}

	Quaternion CubeRotation()
	{
		if (hitNm.y != 0)
		{
			return Quaternion.Euler(90f * ((hitNm.y < 1) ? (-1f) : (0f)), 90f * rotIndex * -hitNm.y, 0f).SnapToRightAngles();
		}
		if (hitNm.x != 0)
		{
			return Quaternion.Euler(0f, hitNm.x * -90f, 90f * rotIndex).SnapToRightAngles();
		}
		if (hitNm.z != 0)
		{
			return Quaternion.Euler(0f, (hitNm.z + 1) * 90f, 90f * rotIndex).SnapToRightAngles();
		}
		return Quaternion.identity;
	}

	bool CanPlace()
	{
		Voxel cube = hit.transform.gameObject.GetComponent<Voxel>();
		if (GlobalVars.IsInMenu()) return false;
		if (hit.transform.gameObject.layer == 8)
		{
			if (hit.transform.tag == "Voxel" || hit.transform.tag == "Floor")
			{
				if (cube.IsValidConnectionPoint(hit.transform.InverseTransformPoint(hit.point).Round()))
				{
					return true;
				}
				else return false;
			}
			else return false;
		}
		else return false;
	}

	bool CanDestroy()
	{
		if (GlobalVars.IsInMenu()) return false;
		if (hit.transform.gameObject.layer == 8)
		{
			if (hit.transform.tag == "Voxel")
			{
				return true;
			}
			else return false;
		}
		else return false;
	}

	void RotIndex(int direction)
	{
		switch (direction)
		{
			case -1:
				if (rotIndex == 0) rotIndex = 3; else rotIndex--;
				break;
			case 1:
				if (rotIndex == 3) rotIndex = 0; else rotIndex++;
				break;
		}
	}
}

