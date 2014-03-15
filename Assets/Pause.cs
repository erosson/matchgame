using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	public GameObject paused;

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (paused.activeSelf) {
				// unpause
				Time.timeScale = 1;
			}
			else {
				// pause
				Time.timeScale = 0;
			}
			paused.SetActive (!paused.activeSelf);
		}
	}
}
