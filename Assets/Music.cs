using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
	// http://answers.unity3d.com/questions/11314/audio-or-music-to-continue-playing-between-scene-c.html
	private static Music _singleton = null;
	public static Music Instance {
		get {
			return _singleton;
		}
	}

	void Start () {
		if (_singleton != null) {
			if (_singleton != this) {
				Destroy (this.gameObject);
			}
		}
		else {
			_singleton = this;
			DontDestroyOnLoad(gameObject);
		}
	}
}
