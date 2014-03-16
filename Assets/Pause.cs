using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	public enum State { Paused, Unpaused };
	public State state = State.Unpaused;

	void OnGUI() {
		if (state == State.Paused) {
			GUILayout.BeginArea(new Rect(Screen.width/4, Screen.height/2, Screen.width/2, Screen.height/2));
			GUILayout.Label("Paused");
			if (GUILayout.Button("Resume")) {
				Resume();
			}
			if (GUILayout.Button("Quit to Title Screen")) {
				QuitToTitle();
			}
			GUILayout.EndArea();
		}
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (state == State.Paused) {
				Resume();
			}
			else {
				DebugUtil.Assert(state == State.Unpaused);
				DoPause();
			}
		}
	}

	private void DoPause() {
		Time.timeScale = 0;
		state = State.Paused;
	}
	
	private void Resume() {
		Time.timeScale = 1;
		state = State.Unpaused;
	}
	
	private void QuitToTitle() {
		Application.LoadLevel("MainMenu");
	}
}
