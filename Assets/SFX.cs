using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SFX : MonoBehaviour {
	public AudioClip grab;
	public AudioClip drop;
	public AudioClip match;
	public AudioClip[] combos;
	public int[] comboThresholds;
	public AudioClip gameover;

	public Grid grid;

	// Use this for initialization
	void Start () {
		DebugUtil.Assert(combos.Length == comboThresholds.Length);
		for (int i=0; i < comboThresholds.Length - 1; i++) {
			DebugUtil.Assert(comboThresholds[i] < comboThresholds[i + 1]);
		}

		//grid.ClearEvent += 
		grid.MatchEvent += OnMatch;
		grid.DropEvent += (a, b, c) => audio.PlayOneShot(drop);
		grid.GrabEvent += (a) => audio.PlayOneShot(grab);
	}

	private void OnMatch(ICollection<Block> blocks) {
		var clip = match;
		for (int i=0; i < combos.Length; i++) {
			if (blocks.Count <= comboThresholds[i]) {
				break;
			}
			clip = combos[i];
		}
		audio.PlayOneShot(clip);
	}
}
