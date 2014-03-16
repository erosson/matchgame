using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public AudioClip select;
	public AudioClip cancel;

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
		// Singletons are icky, but needed to play the sound properly after the menu's destroyed by loadlevel
		Music.Instance.audio.PlayOneShot(select);
		Application.LoadLevel("Game");
	}

	private void Exit() {
		Music.Instance.audio.PlayOneShot(cancel);
		Application.Quit();
	}
}
