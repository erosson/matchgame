#define DEBUG

public class DebugUtil {
	public static void Assert(bool value) {
		Assert(value, null);
	}
	public static void Assert(bool value, string errmsg) {
//# if DEBUG
		if (!value) {
			if (errmsg == null) {
				errmsg = "assertion error";
			}
			throw new System.Exception(errmsg);
		}
//#endif
	}
}
