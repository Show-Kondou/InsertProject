using UnityEngine;
using System.Collections;

public class GameClearDisplay : MonoBehaviour 
{
    // ----- プライベート変数 -----
    private float DisplayTime = 0.0f;
    private GameObject GameClearTextObj;

	// ===== スタート関数 =====
	void Start () 
    {
        // オブジェクト格納
        GameClearTextObj = this.transform.FindChild("GameClearText").gameObject;
	}
	
	// ===== 更新関数 =====
	void Update () 
    {
        DisplayTime += Time.deltaTime;

        // 1秒経ったらクリアテキストを表示
        if (DisplayTime >= 1.0f)
            GameClearTextObj.SetActive(true);
	}
}
