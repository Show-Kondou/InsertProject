using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour {
    /* 描画系 */
    [Header("スプライト(READY?)"), SerializeField]
    private Sprite Ready;
    [Header("スプライト(GO!)"), SerializeField]
    private Sprite Go;
    // イメージ取得
    private Image myImage;

    /* 数値 */
    [Header("演出時間"), SerializeField]
    private float time;
    // 現在時間
    private float nowTime = 0.0F;

    private int drawCnt = 0;

    // Use this for initialization
    void Start () {
        var image = GetComponent<Image>();
        if ( !image ) {
            Debug.LogError("イメージの取得に失敗");
        }
        myImage = image;
        myImage.sprite = Ready;
        Time.timeScale = 0.0F;
	}
	
	// Update is called once per frame
	void Update () {
        float p = nowTime / time;
        Debug.Log(p);
        var col = myImage.color;
        col.a = p;
        myImage.color = col;

        nowTime += Time.unscaledDeltaTime;

        if ( 1.0F <= p ) {
            nowTime = 0.0F;
            drawCnt++;
            myImage.sprite = Go;
        }
        if ( drawCnt >= 2 ) {
            Time.timeScale = 1.0F;
            Destroy( gameObject );
        }


		
	}
}
