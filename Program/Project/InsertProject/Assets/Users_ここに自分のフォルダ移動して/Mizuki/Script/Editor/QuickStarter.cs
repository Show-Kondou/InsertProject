using UnityEditor;

public class QuickStarter{
	// 実行開始/停止
    [MenuItem("AdditionalItems/QuickStarter &s")]
    static void EditStart() {
        if(EditorApplication.isPlaying) {
            EditorApplication.isPlaying = false;
        }else {
            EditorApplication.isPlaying = true;
        }
	}

	// 一時停止
	[MenuItem("AdditionalItems/QuickStarter &a")]
	static void EditStop() {
		if(EditorApplication.isPlaying) {
			if(EditorApplication.isPaused) {
				EditorApplication.isPaused = false;
			} else {
				EditorApplication.isPaused = true;
			}
		}
	}
}
