using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public AudioClip select;
	public AudioClip cancel;

	public void Start() {
	}

	void OnGUI() {
		DebugUtil.ScaleGUI();

		GUILayout.BeginArea(new Rect(DebugUtil.ScreenWidth/4, DebugUtil.ScreenHeight/2, DebugUtil.ScreenWidth/2, DebugUtil.ScreenHeight/2));
		if (GUILayout.Button("Play")) {
			Play();
		}
		if (GUILayout.Button("Feedback")) {
			Feedback();
		}
		if (GUILayout.Button("Options")) {
			Options();
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

	private void Feedback () {
		Music.Instance.audio.PlayOneShot(select);
		Application.OpenURL("http://www.google.com");
		throw new System.NotImplementedException ();
	}

	private void Options () {
		Music.Instance.audio.PlayOneShot(select);
		// #28
		throw new System.NotImplementedException ();
	}

	private void Exit() {
		Music.Instance.audio.PlayOneShot(cancel);
		Application.Quit();
	}
}
