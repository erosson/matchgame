using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour {
	public AudioClip select;
	public AudioClip cancel;
	public MainMenu mainMenu;
	
	void OnGUI() {
		DebugUtil.ScaleGUI();
		
		GUILayout.BeginArea(new Rect(DebugUtil.ScreenWidth/4, DebugUtil.ScreenHeight/2, DebugUtil.ScreenWidth/2, DebugUtil.ScreenHeight/2));
		GUILayout.BeginHorizontal();
		GUILayout.Label ("Music Volume");
		Music.Instance.audio.volume = GUILayout.HorizontalSlider(Music.Instance.audio.volume, 0, 1);
		GUILayout.EndHorizontal();
/*		GUILayout.BeginHorizontal();
		GUILayout.Label ("Sound Effects Volume");
		Music.Instance.audio.volume = GUILayout.HorizontalSlider(Music.Instance.audio.volume, 0, 1);
		GUILayout.EndHorizontal();
*/		if (GUILayout.Button("Back")) {
			Back();
		}
		GUILayout.EndArea();
	}

	private void Back() {
		Music.Instance.audio.PlayOneShot(select);
		enabled = false;
		mainMenu.enabled = true;
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Back();
		}
	}
}
