using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	public enum MatchType { One, Two, Three, Four, Five, Six };
	public Sprite[] sprites;
	public MatchType matchType;
	public enum State { Idle, Matching, Dragging };
	public State state = State.Idle;
	public PauseListener pause;
	public bool IsMatchable {
		get {
			return state == State.Idle;
		}
	}

	public delegate void DropEventHandler(Block block, IntVector2 start, IntVector2 end);
	public event DropEventHandler DropEvent = delegate {};
	
	public delegate void GrabEventHandler(Block block);
	public event GrabEventHandler GrabEvent = delegate {};
	
	private IntVector2 animatedPosition = null;
	public IntVector2 Position {
		get {
			return animatedPosition != null ? animatedPosition : IntVector2.FromVector(transform.position);
		}
		set {
			animatedPosition = value;
			LeanTween.move(gameObject, new Vector2(value.X, value.Y), 0.3f).setOnComplete(() => {
				animatedPosition = null;
			});
		}
	}

	private bool IsAnimating {
		get {
			return animatedPosition != null;
		}
	}

	// based on http://answers.unity3d.com/questions/12322/
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 dragStartPosition;

	void Start() {
		var ordinal = (int)matchType;
		GetComponentInChildren<SpriteRenderer>().sprite = sprites[ordinal];
	}

	void OnMouseDown() {
		if (state == State.Idle && !IsAnimating && !pause.IsPaused) {
			state = State.Dragging;
			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
			dragStartPosition = gameObject.transform.position;
			GrabEvent(this);
		}
	}
	
	void OnMouseDrag() {
		if (state == State.Dragging) {
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
			var diff = curPosition - dragStartPosition;
			if (Mathf.Abs(diff.x) > Mathf.Abs (diff.y)) {
				transform.position = new Vector3(curPosition.x, dragStartPosition.y, dragStartPosition.z);
			}
			else {
				transform.position = new Vector3(dragStartPosition.x, curPosition.y, dragStartPosition.z);
			}
			//transform.position = curPosition;
		}
	}

	void OnMouseUp() {
		if (state == State.Dragging) {
			state = State.Idle;
			// Lock to grid.
			Position = IntVector2.FromVector(transform.position);
			DropEvent(this, IntVector2.FromVector(dragStartPosition), Position);
		}
	}

	public bool IsMatching(Block that) {
		return IsMatchable && that.IsMatchable
			&& matchType == that.matchType;
	}
}
