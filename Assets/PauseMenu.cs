using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	public AudioClip select;
	public AudioClip cancel;

	void OnGUI() {
		DebugUtil.ScaleGUI();
		GUILayout.BeginArea(new Rect(DebugUtil.ScreenWidth/4, DebugUtil.ScreenHeight/2, DebugUtil.ScreenWidth/2, DebugUtil.ScreenHeight/2));
		GUILayout.Label("Paused");
		if (GUILayout.Button("Resume")) {
			Resume();
		}
		if (GUILayout.Button("Restart Level")) {
			RestartLevel();
		}
		if (GUILayout.Button("Quit to Title Screen")) {
			QuitToTitle();
		}
		GUILayout.EndArea();
	}

	public void OnEnable() {
		Music.Instance.audio.PlayOneShot(select);
	}
	
	private void Resume() {
		Music.Instance.audio.PlayOneShot(select);
		Time.timeScale = 1;
		enabled = false;
	}
	
	private void RestartLevel() {
		Music.Instance.audio.PlayOneShot(select);
		Time.timeScale = 1;
		Application.LoadLevel(Application.loadedLevelName);
	}

	private void QuitToTitle() {
		Music.Instance.audio.PlayOneShot(cancel);
		Time.timeScale = 1;
		Application.LoadLevel("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Resume();
		}
	}
}
