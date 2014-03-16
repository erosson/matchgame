using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	public AudioClip gameOver;
	public delegate void TimeOverEvent();
	public event TimeOverEvent TimeOver = delegate {};
	public Score score;
	public SceneParameter gameOverMenuParam;

	public int timelimitSeconds = 180;

	void Start() {
		TimeOver += OnTimeOver;

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

	private void OnTimeOver() {
		Debug.Log("timeover!");
		Music.Instance.audio.PlayOneShot(gameOver);
		gameOverMenuParam.value = new GameOverData(score.score);
		DontDestroyOnLoad(gameOverMenuParam);
		Application.LoadLevel("GameOverMenu");
	}

	private void Display(System.TimeSpan remaining) {
		guiText.text = string.Format("{0:0}:{1:00}", remaining.Minutes, remaining.Seconds, remaining.Milliseconds);
	}
}
