using UnityEngine;
using System.Collections;

public class GameOverMenu : MonoBehaviour {
	public AudioClip select;
	public AudioClip cancel;

	private GameOverData data;

	void Start() {
		// by default, this.data is dummy data so we can easily test this scene,
		// but usually it's passed as a "parameter" with DontDestroyObjectOnLoad
		var obj = GameObject.Find("/GameOverMenuParam");
		if (obj == null) {
			data = new GameOverData(9999);
		}
		else {
			data = obj.GetComponent<SceneParameter>().value as GameOverData;
			DestroyObject(obj);
		}
	}
	
	void OnGUI() {
		DebugUtil.ScaleGUI();
		GUILayout.BeginArea(new Rect(DebugUtil.ScreenWidth/4, DebugUtil.ScreenHeight/2, DebugUtil.ScreenWidth/2, DebugUtil.ScreenHeight/2));
		GUILayout.Label("Game Over!");
		GUILayout.Label("Score: " + string.Format("{0:N0}", data.score));
		if (GUILayout.Button("Restart Level")) {
			RestartLevel();
		}
		if (GUILayout.Button("Quit to Title Screen")) {
			QuitToTitle();
		}
		GUILayout.EndArea();
	}
	
	private void RestartLevel() {
		Music.Instance.audio.PlayOneShot(select);
		Application.LoadLevel("Game");
	}
	
	private void QuitToTitle() {
		Music.Instance.audio.PlayOneShot(cancel);
		Application.LoadLevel("MainMenu");
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			QuitToTitle();
		}
	}
}
