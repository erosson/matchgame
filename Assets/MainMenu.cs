using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	void OnGUI() {
		GUILayout.BeginArea(new Rect(Screen.width/4, Screen.height/2, Screen.width/2, Screen.height/2));
		if (GUILayout.Button("Play")) {
			Play();
		}
		if (GUILayout.Button("Exit")) {
			Exit();
		}
		GUILayout.EndArea();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Exit();
		}
	}

	private void Play () {
		Application.LoadLevel("Game");
	}

	private void Exit() {
		Application.Quit();
	}
}
