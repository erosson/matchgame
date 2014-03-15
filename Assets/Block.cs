using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	public enum MatchType { One, Two, Three, Four, Five, Six };
	public Sprite[] sprites;
	public MatchType matchType;
	public bool IsMatchable = true;

	public delegate void DropEventHandler(Block block, IntVector2 start, IntVector2 end);
	public event DropEventHandler DropEvent = delegate {};

	public IntVector2 Position {
		get {
			return IntVector2.FromVector(transform.position);
		}
		set {
			transform.position = new Vector3(value.X, value.Y, transform.position.z);
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
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		dragStartPosition = gameObject.transform.position;
	}
	
	void OnMouseDrag() {
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

	void OnMouseUp() {
		// Lock to grid. TODO animate this?
		Position = IntVector2.FromVector(transform.position);
		DropEvent(this, IntVector2.FromVector(dragStartPosition), Position);
	}

	public bool IsMatching(Block that) {
		return IsMatchable && that.IsMatchable
			&& matchType == that.matchType;
	}
}
