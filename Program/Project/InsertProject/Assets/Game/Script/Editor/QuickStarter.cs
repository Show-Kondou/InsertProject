//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
/*	QuickStarter
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	エディタ拡張
//	Alt+Sで実行、停止
//	実行中にAlt+Aで一時停止
//	
//	//==今後追加予定==//
//
//==================================================
//	作成日：2017/03/27
*/
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 


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
