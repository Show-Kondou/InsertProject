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

	[Header("ストップゲーム"),SerializeField]
	private StopGame stopGame;

    /* 数値 */
    [Header("演出時間"), SerializeField]
    private float time;
    // 現在時間
    private float nowTime = 0.0F;

    private int drawCnt = 0;

    private float posz;
    [SerializeField]
    private float zrange = 0.0F;

    // Use this for initialization
    void Start () {
        var image = GetComponent<Image>();
        if ( !image ) {
            Debug.LogError("イメージの取得に失敗");
        }
		if( !stopGame ) {
			Debug.LogError("ストップゲームの取得に失敗");
		}
        myImage = image;
        myImage.sprite = Ready;
        Time.timeScale = 0.0F;
        posz = myImage.rectTransform.localPosition.z;
	}
	
	// Update is called once per frame
	void Update () {
		stopGame.StopGameEvent();
        float p = nowTime / time;


        var col = myImage.color;
        col.a = p;
        myImage.color = col;

        var pos = myImage.rectTransform.localPosition;
        pos.z = posz + zrange * p;
        myImage.rectTransform.localPosition = pos;

        nowTime += Time.unscaledDeltaTime;

        if ( 1.0F <= p ) {
            nowTime = 0.0F;
            drawCnt++;
            pos = myImage.rectTransform.localPosition;
            pos.z = posz;
            myImage.rectTransform.localPosition = pos;
            col = myImage.color;
            col.a = 0.0F;
            myImage.color = col;
            myImage.sprite = Go;
        }
        if ( drawCnt >= 2 ) {
            Time.timeScale = 1.0F;
            Destroy( gameObject );
			stopGame.StartGameEvent();
        }
	}
}
