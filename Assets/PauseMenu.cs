﻿using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	void OnGUI() {
		GUILayout.BeginArea(new Rect(Screen.width/4, Screen.height/2, Screen.width/2, Screen.height/2));
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
	
	private void Resume() {
		Time.timeScale = 1;
		enabled = false;
	}
	
	private void RestartLevel() {
		Time.timeScale = 1;
		Application.LoadLevel(Application.loadedLevelName);
	}

	private void QuitToTitle() {
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