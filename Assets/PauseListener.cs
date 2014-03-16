using UnityEngine;
using System.Collections;

public class PauseListener : MonoBehaviour {
	private PauseMenu pauseMenu {
		get {
			return GetComponent<PauseMenu>();
		}
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.enabled) {
			Pause();
		}
	}

	private void Pause() {
		Time.timeScale = 0;
		pauseMenu.enabled = true;
	}
}
