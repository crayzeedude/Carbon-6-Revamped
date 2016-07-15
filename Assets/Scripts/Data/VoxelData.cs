using UnityEngine;
using System.Collections;

[System.Serializable]
public class VoxelData {
	public VoxelUID UID;
	public VoxelPosition position;
	public VoxelRotation rotation;
	public VoxelData(int id, VoxelPosition position, VoxelRotation rotation)
	{
		UID = new VoxelUID(id, position, rotation);
		this.position = position;
		this.rotation = rotation;
	}
}

[System.Serializable]
public struct VoxelUID
{
	public string UID;
	public VoxelUID(int id, VoxelPosition position, VoxelRotation rotation)
	{
		UID = "";
		UID += id.ToString().PadLeft(5, '0');
		UID += position.ToString();
		UID += rotation.ToString();
	}
	public VoxelPosition DecodePosition()
	{
		string id = UID.Substring(4, 9);
		float xF; float.TryParse(id.Substring(4, 3), out xF);
		float yF; float.TryParse(id.Substring(7, 3), out yF);
		float zF; float.TryParse(id.Substring(10, 3), out zF);
		return new VoxelPosition(xF, yF, zF);
	}
	public int DecodeID()
	{
		string id = UID.Substring(0, 5);
		int i;
		int.TryParse(id, out i);
		return i;
	}
}

[System.Serializable]
public struct VoxelPosition
{
	public float x;
	public float y;
	public float z;
	public VoxelPosition(float x, float y, float z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public Vector3 ToVector3() { return new Vector3(x, y, z); }

	public override string ToString()
	{
		return x.ToString().PadLeft(3, '0') + y.ToString().PadLeft(3, '0') + z.ToString().PadLeft(3, '0');
	}
}

[System.Serializable]
public struct VoxelRotation
{
	public float x;
	public float y;
	public float z;
	public VoxelRotation(float x, float y, float z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public Quaternion ToQuaternion() { return Quaternion.Euler(x, y, z); }

	public override string ToString()
	{
		return x.ToString().PadLeft(3, '0') + y.ToString().PadLeft(3, '0') + z.ToString().PadLeft(3, '0');
	}
}