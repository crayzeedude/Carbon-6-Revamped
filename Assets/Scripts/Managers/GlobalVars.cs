using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalVars : MonoBehaviour {
	[HideInInspector]

	public enum Menu
	{
		None = 0,
		Pause = 1,
		Inventory = 2,
		Options = 3,
		Invalid = -1
	}

	public enum Category
	{
		Chassis = 0,
		Movement = 1,
		Weapon = 2
	}

	public static Menu CurrentMenu = Menu.None;

	public static List<Voxel> voxels = new List<Voxel>();

	public static bool IsInMenu()
	{
		return (!CurrentMenu.Equals(Menu.None));	// Returns true if a menu is active, false if the player is in the build area
	}
}
