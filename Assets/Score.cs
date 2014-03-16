using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {
	public Grid grid;
	public ScoreToast toast;

	private long _score = 0;
	public long score {
		get {
			return _score;
		}
	}

	public delegate void ScoreChangeHandler(long dScore, long oldScore, long newScore);
	public event ScoreChangeHandler ScoreChange = delegate {};

	// Use this for initialization
	void Start () {
		grid.MatchEvent += OnMatch;
		ScoreChange += (dScore, oldScore, newScore) =>
			GA.API.Design.NewEvent("Match:ScoreChange", dScore);
	}
	
	private void OnMatch(HashSet<Block> blocks) {
		var dScore = (long) Mathf.Pow(blocks.Count - grid.minMatch + 1, 1.3f) * 100;
		ScoreChange(dScore, score, score + dScore);
		_score += dScore;
		CreateToast(blocks, dScore);
	}

	private void CreateToast(HashSet<Block> blocks, long score) {
		// one-iteration loop. Any block will do. I dunno how enumerators work, lolol
		foreach (var source in blocks) {
			// GUIText uses viewport coordinates (0-1), other gameobjects use world coordinates. Ugh
			Vector3 pos = Camera.main.WorldToViewportPoint(source.transform.position);
			var inst = Instantiate(toast.gameObject, pos, Quaternion.identity) as GameObject;
			//inst.transform.parent = grid.transform;
			inst.SetActive(true);
			inst.GetComponentInChildren<GUIText>().text = FormatScore(score);
			break;
		}
	}

	private string FormatScore(long score) {
		return string.Format("{0:N0}", score);
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = "Score: " + FormatScore(score);
	}
}
