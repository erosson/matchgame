using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public AudioClip select;
	public AudioClip cancel;
	public OptionsMenu optionsMenu;

	private string url = "http://orbitris.zealgame.com";

	void OnGUI() {
		DebugUtil.ScaleGUI();

		GUILayout.BeginArea(new Rect(DebugUtil.ScreenWidth/4, DebugUtil.ScreenHeight/2, DebugUtil.ScreenWidth/2, DebugUtil.ScreenHeight/2));
		if (GUILayout.Button("Play")) {
			Play();
		}
		if (GUILayout.Button("Options")) {
			Options();
		}
		if (GUILayout.Button("Feedback")) {
			Feedback();
		}
		if (GUILayout.Button(url)) {
			Web();
		}
		// Exit button does nothing in the web player
		if (!Application.isWebPlayer && !Application.isEditor) {
			if (GUILayout.Button("Exit")) {
				Exit();
			}
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
		// TODO redirect from orbitris.zealgame.com/feedback
		Application.OpenURL("https://docs.google.com/a/erosson.org/forms/d/12V9Vz-lRQvMuMg91p_AEgeOHcjgDxPt399t_MIHFVig/viewform");
	}

	private void Web () {
		Music.Instance.audio.PlayOneShot(select);
		Application.OpenURL(url);
	}

	private void Options () {
		Music.Instance.audio.PlayOneShot(select);
		enabled = false;
		optionsMenu.enabled = true;
	}

	private void Exit() {
		Music.Instance.audio.PlayOneShot(cancel);
		Application.Quit();
	}
}
