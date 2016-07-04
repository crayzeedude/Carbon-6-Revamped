using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameData {
	public static void Save(string filename, object objectToSave, string objName)
	{
		try
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "/" + filename);
			bf.Serialize(file, objectToSave);
			file.Close();
			Debug.Log(objName + " saved to " + filename + " successfully.");
		}
		catch (Exception e)
		{
			Debug.Log("Error saving data.");
		}
	}
	public static void Save(string filename, object objectToSave) { Save(filename, objectToSave, "Object"); }

	public static void Load<T>(string filename, ref T objectToLoad, string objName)
	{
		try
		{
			if (File.Exists(Application.persistentDataPath + "/" + filename))
			{
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "/" + filename, FileMode.Open);
				objectToLoad = (T)bf.Deserialize(file);
				file.Close();
				Debug.Log(objName + " loaded from " + filename + " successfully.");
			}
			else Debug.Log("Requested file \"" + filename + "\" not found.");
		}
		catch (Exception e)
		{
			Debug.Log("Error loading data.");
		}
	}
	public static void Load<T>(string filename, ref T objectToLoad) { Load(filename, ref objectToLoad, "Object"); }
}
