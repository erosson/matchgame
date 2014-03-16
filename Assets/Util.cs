#define DEBUG
using UnityEngine;

public class DebugUtil {
	public static void Assert(bool value) {
		Assert(value, null);
	}
	public static void Assert(bool value, string errmsg) {
# if DEBUG
		if (!value) {
			if (errmsg == null) {
				errmsg = "assertion error";
			}
			throw new System.Exception(errmsg);
		}
#endif
	}

	public static void ScaleGUI() {
		// http://forum.unity3d.com/threads/7098-GUI-matrix
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(0, new Vector3(0, 1, 0)), new Vector3(GUIScaleFactor, GUIScaleFactor, 1));
	}

	public static float GUIScaleFactor {
		get {
			// TODO
			return 2f;
		}
	}
	
	public static int ScreenWidth {
		get {
			return (int) (Screen.width / GUIScaleFactor);
		}
	}
	
	public static int ScreenHeight {
		get {
			return (int) (Screen.height / GUIScaleFactor);
		}
	}
}
