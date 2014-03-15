using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {
	public Grid grid;

	private long score = 0;

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
		score += dScore;
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = "Score: " + string.Format("{0:N0}", score);
	}
}
