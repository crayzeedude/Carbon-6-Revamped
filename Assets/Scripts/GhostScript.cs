using UnityEngine;
using System.Collections;

public class GhostScript : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider c)
	{
		Debug.Log("Enter");
		Debug.Log(c.name);
		//if (c.tag != "Floor")
		//player.GetComponent<PlayerGarage>().IsColliding = true;//(c.tag != "Floor");
	}

	void OnTriggerExit(Collider c)
	{
		Debug.Log("Exit");
		//player.GetComponent<PlayerGarage>().IsColliding = false;
	}
}
