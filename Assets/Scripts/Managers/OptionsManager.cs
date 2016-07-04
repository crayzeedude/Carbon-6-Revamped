using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class OptionsManager : MonoBehaviour {
	public int QualityLevel;
	public int FPSCapLevel;
	public bool CapFPS;
	public bool VSync;

	public Dropdown QualitySelector;
	public Dropdown FPSSelector;
	public Toggle FPSCapToggle;
	public Toggle VSyncToggle;

	void Start()
	{
		OptionsData.Load();

		QualityLevel = OptionsData.options.QualityLevel;
		FPSCapLevel = OptionsData.options.FPSCapLevel;
		CapFPS = OptionsData.options.CapFPS;
		VSync = OptionsData.options.VSync;

		UpdateOptions();

		ApplyVideoSettings();
		
	}

	public void QualityDropdownChanged()
	{
		QualityLevel = QualitySelector.value;
	}

	public void FPSCapDropdownChanged()
	{
		FPSCapLevel = FPSSelector.value;
	}

	public void FPSCapToggled()
	{
		//Debug.Log("FPS Cap toggled to " + FPSCapToggle.GetComponent<Toggle>().isOn);
		CapFPS = FPSCapToggle.isOn;
	}

	public void VSyncToggled()
	{
		VSync = VSyncToggle.isOn;
		FPSCapToggle.interactable = !VSync;
		FPSSelector.interactable = !VSync;	
	}

	public void ApplyClicked()
	{
		ApplyOptions();
		GetComponent<GarageUIManager>().ToggleOptionsMenu();
	}

	public void ApplyOptions()
	{
		OptionsData.options.CapFPS = CapFPS;
		OptionsData.options.FPSCapLevel = FPSCapLevel;
		OptionsData.options.QualityLevel = QualityLevel;
		OptionsData.options.VSync = VSync;

		OptionsData.Save();

		ApplyVideoSettings();
	}

	public void CancelOptions()
	{
		QualityLevel = OptionsData.options.QualityLevel;
		FPSCapLevel = OptionsData.options.FPSCapLevel;
		CapFPS = OptionsData.options.CapFPS;
		VSync = OptionsData.options.VSync;
		GetComponent<GarageUIManager>().ToggleOptionsMenu();
	}

	void UpdateOptions()
	{
		QualitySelector.value = QualityLevel;
		FPSSelector.value = FPSCapLevel;
		FPSCapToggle.isOn = CapFPS;
		VSyncToggle.isOn = VSync;
		FPSCapToggle.interactable = !VSync;
		FPSSelector.interactable = !VSync;
	}

	public void ApplyVideoSettings()
	{
		QualitySettings.SetQualityLevel(QualityLevel, true);
		QualitySettings.vSyncCount = Convert.ToInt32(VSync);
		Application.targetFrameRate = (CapFPS) ? ((int)Math.Pow(2, FPSCapLevel) * 30) : -1;
		Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
	}
}