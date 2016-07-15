using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class PlayerGarage : MonoBehaviour {
	public Vector3 hitNm;
	public Vector3 hitPoint;
	public Vector3 hitPointInv;
	public Vector3 hitPointInvNm;

	// Build distance
	public float buildDist;

	public GameObject ghost;
	public GameObject HandVoxel;

	int rotIndex = 0;
	Vector3 angles = Vector3.zero;

	public LayerMask layersToRaycast;

	//The data that the raycast hit.
	RaycastHit hit;

	float BuildDelay = 0f;
	float BuildHoldDelay = 0f;
	float DestroyDelay = 0f;
	float DestroyHoldDelay = 0f;

	void Start()
	{
		Debug.Log(VoxelManager.GetVoxel(0).GetComponent<Renderer>().bounds.size.normalized);
	}

	// Update is called once per frame
	void Update () {
		if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)), out hit, buildDist, layersToRaycast))
		{
			hitNm = hit.normal.Round();
			hitPoint = hit.point;
			hitPointInv = hit.transform.InverseTransformPoint(hit.point);
			hitPointInvNm = hit.transform.InverseTransformPoint(hit.point).NormalizeOnAxis(hitNm);
			//Debug.Log(hit.normal.ToString() + " " + hit.normal.Round().ToString());
			if (CanPlace())
			{
				ghost.transform.position = CubePosition();
				ghost.transform.rotation = CubeRotation();

				TryPlace();
			}
			else { ghost.transform.position = new Vector3(0, -500, 0); }
			if (CanDestroy())
			{
				if (Input.GetMouseButtonDown(2))
				{
					InventoryManager.Select(hit.transform.gameObject.GetComponent<Voxel>().id);
				}

				TryDestroy();
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
		switch (InventoryManager.CurrentVoxel.GetComponent<Voxel>().RestrictedRotation)
		{
			case 0:
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
				break;
			case 1:
				return Quaternion.identity;//return Quaternion.Euler(0f, 90f + (90f * hitNm.x), 0f).SnapToRightAngles();
			case 2:
				return Quaternion.identity;
		}
		return Quaternion.identity;
	}

	bool CanPlace()
	{
		//return InventoryManager.CurrentVoxel.GetComponent<Voxel>().CanPlace(hit);
		Voxel cube = hit.transform.gameObject.GetComponent<Voxel>();
		if (GlobalVars.IsInMenu()) return false;
		if (hit.transform.gameObject.layer == 8)
		{
			if (hit.transform.tag == "Voxel" || hit.transform.tag == "Floor")
			{
				if (cube.IsValidConnectionPoint((hit.point - hit.transform.position).NormalizeOnAxis(hitNm)))//if (cube.IsValidConnectionPoint(hitNm))//if (cube.IsValidConnectionPoint(hit.transform.InverseTransformPoint(hit.point).NormalizeOnAxis(hitNm)))
				{
					if (cube.IsValidSide(InventoryManager.CurrentVoxel.GetComponent<Voxel>(), hitNm))
					{
						return true;
					}
					else return false;
				}
				else return false;// { Debug.Log(hit.transform.InverseTransformPoint(hit.point).ToString() + " " + hit.transform.InverseTransformPoint(hit.point + (Vector3.one * 0.001f)).Round().ToString() + " " + hit.point.ToString()); return false;}
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

	public void UpdatePreviews()
	{
		ghost.GetComponent<MeshFilter>().mesh = InventoryManager.CurrentVoxel.GetComponent<MeshFilter>().sharedMesh;
		HandVoxel.GetComponent<MeshFilter>().mesh = ghost.GetComponent<MeshFilter>().mesh;
		ghost.GetComponent<MeshCollider>().sharedMesh = InventoryManager.CurrentVoxel.GetComponent<MeshCollider>().sharedMesh;
		HandVoxel.transform.localScale = ghost.GetComponent<Renderer>().bounds.size.normalized;
	}

	void TryPlace()
	{
		if (Input.GetMouseButton(0))
		{
			if (BuildDelay == 0f)
			{
				GameObject voxel = (GameObject)Instantiate(InventoryManager.CurrentVoxel, CubePosition(), ghost.transform.rotation);//InventoryManager.CurrentVoxel.transform.rotation.SnapToRightAngle());
			}
			if (BuildDelay >= 1f)
			{
				if (BuildHoldDelay >= 0.1f)
				{
					GameObject voxel = (GameObject)Instantiate(InventoryManager.CurrentVoxel, CubePosition(), ghost.transform.rotation);
					BuildHoldDelay = 0f;
				}
			}
			BuildHoldDelay += Time.deltaTime;
			BuildDelay += Time.deltaTime;
		} else { BuildDelay = 0f; BuildHoldDelay = 0f; }
	}

	void TryDestroy()
	{
		if (Input.GetMouseButton(1))
		{
			if (DestroyDelay == 0f)
			{
				Destroy(hit.transform.gameObject);
			}
			if (DestroyDelay >= 1f)
			{
				if (DestroyHoldDelay >= 0.1f)
				{
					Destroy(hit.transform.gameObject);
					DestroyHoldDelay = 0f;
				}
			}
			DestroyHoldDelay += Time.deltaTime;
			DestroyDelay += Time.deltaTime;
		} else { DestroyDelay = 0f; DestroyHoldDelay = 0f; }
	}
}

