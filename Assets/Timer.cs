using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	public delegate void TimeOverEvent();
	public event TimeOverEvent TimeOver = delegate {};

	public int timelimitSeconds = 180;

	void Start() {
		TimeOver += () => Debug.Log ("timeover!");

		StartCoroutine(Countdown(new System.TimeSpan(0, 0, timelimitSeconds)));
	}

	private IEnumerator Countdown(System.TimeSpan remaining) {
		// Instead of using a starttime, this accounts for pauses/timescale
		Display(remaining);
		var second = new System.TimeSpan(0, 0, 1);
		while (remaining.TotalSeconds > 0) {
			yield return new WaitForSeconds(1);
			remaining = remaining.Subtract(second);
			Display(remaining);
		}
		TimeOver();
	}

	private void Display(System.TimeSpan remaining) {
		guiText.text = string.Format("{0:0}:{1:00}", remaining.Minutes, remaining.Seconds, remaining.Milliseconds);
	}
}
