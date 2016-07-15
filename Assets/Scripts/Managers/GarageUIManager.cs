using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GarageUIManager : MonoBehaviour {
	public GameObject PauseMenu;
	public GameObject InventoryMenu;
	public GameObject PlayerUI;
	public GameObject OptionsMenu;
	public GameObject VoxelSelectButton;


	void Start () {
		SwitchMenu();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) { TogglePauseMenu(); }
		else if (Input.GetKeyDown(KeyCode.Q)) { ToggleInventoryMenu(); }
		else if (Input.GetKeyDown(KeyCode.E) && !GlobalVars.IsInMenu()) if (WorldTransitionManager.PrepareVoxels()) WorldTransitionManager.LoadMap();
	}

	public void DebugButton()
	{
		//Debug.Log("Options Data: " + OptionsData.options.ToString());
		//Debug.Log("OptionsManager Data: " + GetComponent<OptionsManager>().QualityLevel.ToString() + " " + GetComponent<OptionsManager>().CapFPS.ToString() + " " + GetComponent<OptionsManager>().FPSCapLevel.ToString());
	}

	public void OnResumeClicked()
	{
		TogglePauseMenu();
	}

	public void OnOptionsClicked()
	{

		SwitchMenu();
	}

	void LoadButtons()
	{
		int index = 0;
		int x = -4;
		//int x = 0;
		int y = 12;
		//int y = 0;

		foreach (GameObject b in VoxelManager.voxels)
		{
			int tmpInt = index;
			GameObject button = Instantiate(VoxelSelectButton);
			RectTransform bR = button.GetComponent<RectTransform>();
			//bR.SetParent(ScrollViewContent.transform);	// Parent button to Scroll View
			bR.SetParent(InventoryMenu.transform);	// Parent button to Inventory UI
			bR.GetChild(0).gameObject.GetComponent<Text>().text = b.GetComponent<Voxel>().DisplayName;	// Set button's text to voxel's display name
			bR.localPosition = new Vector3((x * 170) + 50, y * 30, 0);
			//bR.localPosition = new Vector3((x * 170), y * 30, 0);
			bR.localScale = Vector3.one;
			button.GetComponent<Button>().onClick.AddListener(delegate { OnInvButtonClicked(tmpInt);});
			x++;
			//if ((x) / 4 == 1) { y--; x = -1; }
			index++;
		}
	}

	public void ExitGame()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
		Application.Quit();
	}

	public void TogglePauseMenu()
	{
		GlobalVars.CurrentMenu = (GlobalVars.CurrentMenu.Equals(GlobalVars.Menu.None) && !GlobalVars.CurrentMenu.Equals(GlobalVars.Menu.Options)) ? GlobalVars.Menu.Pause : GlobalVars.Menu.None;
		SwitchMenu();
	}

	public void ToggleInventoryMenu()
	{
		GlobalVars.CurrentMenu = (GlobalVars.CurrentMenu.Equals(GlobalVars.Menu.None)) ? GlobalVars.Menu.Inventory : (GlobalVars.CurrentMenu.Equals(GlobalVars.Menu.Inventory)) ? GlobalVars.Menu.None : GlobalVars.CurrentMenu;
		SwitchMenu();
	}

	public void ToggleOptionsMenu()
	{
		GlobalVars.CurrentMenu = (GlobalVars.CurrentMenu.Equals(GlobalVars.Menu.Pause)) ? GlobalVars.Menu.Options : (GlobalVars.CurrentMenu.Equals(GlobalVars.Menu.Options)) ? GlobalVars.Menu.Pause : GlobalVars.CurrentMenu;
		SwitchMenu();
	}

	public void SwitchMenu()
	{
		PauseMenu.SetActive(GlobalVars.CurrentMenu.Equals(GlobalVars.Menu.Pause));
		InventoryMenu.SetActive(GlobalVars.CurrentMenu.Equals(GlobalVars.Menu.Inventory));
		PlayerUI.SetActive(GlobalVars.CurrentMenu.Equals(GlobalVars.Menu.None));
		OptionsMenu.SetActive(GlobalVars.CurrentMenu.Equals(GlobalVars.Menu.Options));
		PlayerGarageMouse.CursorLock(GlobalVars.IsInMenu());
	}

	/*public void TogglePauseMenu() // Called when the Escape key is pressed or the Resume button is clicked
	{
		switch (GlobalVars.CurrentMenu)
		{
			case GlobalVars.Menu.None:
				GlobalVars.CurrentMenu = GlobalVars.Menu.Pause;
				break;
			case GlobalVars.Menu.Pause:
			case GlobalVars.Menu.Inventory:
				GlobalVars.CurrentMenu = GlobalVars.Menu.None;
				break;
			case GlobalVars.Menu.Options:	// User shouldn't be able to back out of options menu using Escape key
				break;
			default:
				GlobalVars.CurrentMenu = GlobalVars.Menu.Invalid;
				break;
		}
		SwitchMenu();
	}*/

	/*public void ToggleInventoryMenu()	// Called when the Q key is pressed or a cube is selected
	{
		switch (GlobalVars.CurrentMenu)
		{
			case GlobalVars.Menu.None:
				GlobalVars.CurrentMenu = GlobalVars.Menu.Inventory;
				break;
			case GlobalVars.Menu.Inventory:
				GlobalVars.CurrentMenu = GlobalVars.Menu.None;
				break;
			case GlobalVars.Menu.Pause:
				break;
			default:
				GlobalVars.CurrentMenu = GlobalVars.Menu.Invalid;
				break;
		}
		SwitchMenu();
	}*/

	/*public void ToggleOptionsMenu()
	{
		switch (GlobalVars.CurrentMenu)
		{
			case GlobalVars.Menu.Pause:
				GlobalVars.CurrentMenu = GlobalVars.Menu.Options;
				break;
			case GlobalVars.Menu.Options:
				GlobalVars.CurrentMenu = GlobalVars.Menu.Pause;
				break;
			default:
				GlobalVars.CurrentMenu = GlobalVars.Menu.Invalid;
				break;
		}
		SwitchMenu();
	}*/

	/*public void SwitchMenu()		// Processes menu changes
	{
		switch (GlobalVars.CurrentMenu)
		{
			case GlobalVars.Menu.None:
				PauseMenu.SetActive(false);
				PlayerUI.SetActive(true);
				InventoryMenu.SetActive(false);
				OptionsMenu.SetActive(false);
				PlayerGarageMouse.CursorLock(false);
				break;
			case GlobalVars.Menu.Pause:
				PauseMenu.SetActive(true);
				PlayerUI.SetActive(false);
				InventoryMenu.SetActive(false);
				OptionsMenu.SetActive(false);
				PlayerGarageMouse.CursorLock(true);
				break;
			case GlobalVars.Menu.Inventory:
				PauseMenu.SetActive(false);
				PlayerUI.SetActive(false);
				InventoryMenu.SetActive(true);
				OptionsMenu.SetActive(false);
				PlayerGarageMouse.CursorLock(true);
				break;
			case GlobalVars.Menu.Options:
				PauseMenu.SetActive(false);
				PlayerUI.SetActive(false);
				InventoryMenu.SetActive(false);
				OptionsMenu.SetActive(true);
				PlayerGarageMouse.CursorLock(true);
				break;
		}
	}*/

	void OnInvButtonClicked(int id)		// Called whenever an inventory button is clicked
	{
		InventoryManager.Select(id);
		ToggleInventoryMenu();
	}
}
