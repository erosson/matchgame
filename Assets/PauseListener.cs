using UnityEngine;
using System.Collections;

public class PauseListener : MonoBehaviour {
	private PauseMenu pauseMenu {
		get {
			return GetComponent<PauseMenu>();
		}
	}

	public bool IsPaused {
		get {
			return pauseMenu.enabled;
		}
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && !IsPaused) {
			Pause();
		}
	}

	private void Pause() {
		Time.timeScale = 0;
		pauseMenu.enabled = true;
	}
}
