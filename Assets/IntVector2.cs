using UnityEngine;
using System.Collections;

public class IntVector2 {
	public readonly int X;
	public readonly int Y;

	public IntVector2(int x, int y) {
		X = x;
		Y = y;
	}
	
	public static IntVector2 FromVector(Vector2 vector) {
		return new IntVector2(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
	}
	
	public static IntVector2 FromVector(Vector3 vector) {
		return new IntVector2(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
	}

	public override string ToString() {
		return base.ToString() + "(" + X + ", " + Y + ")";
	}

	public override bool Equals(object that) {
		if (that == null || that.GetType() != this.GetType()) {
			return false;
		}
		IntVector2 that_ = (IntVector2) that;
		return this.X == that_.X && this.Y == that_.Y;
	}

	public override int GetHashCode() {
		return X.GetHashCode() ^ Y.GetHashCode();
	}
	
	public static IntVector2 operator +(IntVector2 a, IntVector2 b) {
		return new IntVector2(a.X + b.X, a.Y + b.Y);
	}
	
	public static IntVector2 operator -(IntVector2 a) {
		return new IntVector2(-a.X, -a.Y);
	}

	public static IntVector2 operator -(IntVector2 a, IntVector2 b) {
		return a + -b;
	}
}
