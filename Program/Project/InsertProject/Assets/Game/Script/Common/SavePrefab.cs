#if UNITY_EDITOR

using UnityEditor;

/// <summary>
/// 全プレハブの任意保存コマンド
/// </summary>
public class Util : Editor {
	[UnityEditor.MenuItem( "Edit/SavePrefab %&s" )]
	static void SavePrefab() {
		AssetDatabase.SaveAssets();
	}
}

#endif