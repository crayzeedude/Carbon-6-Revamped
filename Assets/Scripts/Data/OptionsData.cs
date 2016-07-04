using UnityEngine;
using System.Collections;

//[System.Serializable]
public static class OptionsData {
	[System.Serializable]
	public struct Options
	{
		public bool VSync { get; set; }
		public bool CapFPS { get; set; }
		public int FPSCapLevel { get; set; }
		public int QualityLevel { get; set; }

		public static Options Default = new Options(true, false, 1, 0);

		private Options(bool VSync, bool CapFPS, int FPSCapLevel, int QualityLevel)
		{
			this.VSync = VSync;
			this.CapFPS = CapFPS;
			this.FPSCapLevel = FPSCapLevel;
			this.QualityLevel = QualityLevel;
		}

		public override string ToString()
		{
			return "OptionsData";
			//return (QualityLevel.ToString() + " " + CapFPS.ToString() + " " + FPSCapLevel.ToString() + " " + VSync.ToString());
		}
	}

	public static Options options = Options.Default;

	public static void Save()
	{
		GameData.Save("options.bin", options, "OptionsData");
	}

	public static void Load()
	{
		GameData.Load("options.bin", ref options, "OptionsData");
	}
}
