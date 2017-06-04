using UnityEditor;

public class QuickStarter{
    [MenuItem("AdditionalItems/QuickStarter &s")]
    static void AudioNameCreate() {
        if(EditorApplication.isPlaying) {
            EditorApplication.isPlaying = false;
        }else {
            EditorApplication.isPlaying = true;
        }
    }
}
