using UnityEngine;
using System.Collections;

public class ScalableUIComponent : MonoBehaviour {
	Resolution res = Screen.currentResolution;
	float height;
	float width;
	// Use this for initialization
	void Start () {
		height = GetComponent<RectTransform>().rect.height;
		width = GetComponent<RectTransform>().rect.width;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
