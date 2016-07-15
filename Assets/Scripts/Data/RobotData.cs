using System.Collections.Generic;

[System.Serializable]
public static class RobotData {
	public static List<VoxelData> voxels = new List<VoxelData>();
	public static List<VoxelData> WorldVoxels = new List<VoxelData>();

	public static void Save()
	{
		GameData.Save("robot.bin", voxels, "RobotData");
	}

	public static void Load()
	{
		GameData.Load("robot.bin", ref voxels, "RobotData");
	}

	public static void SaveWorld()
	{
		GameData.Save("robotworld.bin", WorldVoxels, "WorldRobotData");
	}

	public static void LoadWorld()
	{
		GameData.Load("robotworld.bin", ref WorldVoxels, "WorldRobotData");
	}
}
