using UnityEngine;

public class BackgroundTint : MonoBehaviour {
	public GameObject tint;

	void Update()
	{
		tint.SetActive(GlobalVars.IsInMenu());
	}
}
