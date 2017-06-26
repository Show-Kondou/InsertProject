using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScroll : MonoBehaviour {

    public float ScrollSpeed = 400f;    //スクロールスピード
    float PosX = 0f;                    //X座標位置

    [SerializeField]
    ButtonScroll CButtonScroll;

    [SerializeField]
    Canvas CCanvas;                     //初期値取得用

	// Use this for initialization
	void Start () {
        //初期位置
        PosX = (CCanvas.GetComponent<RectTransform>().sizeDelta.x / 2f) + (GetComponent<RectTransform>().sizeDelta.x / 2f);
        transform.localPosition = new Vector2(PosX, transform.localPosition.y);
	}

    /// <summary>
    /// クリアタイムスクロール
    /// </summary>
    public void Scroll()
    {
        //左にクリアタイムスクロール
        if (PosX >= 0)
        {
            PosX -= ScrollSpeed * Time.deltaTime;
            transform.localPosition = new Vector2(PosX, transform.localPosition.y);
        }
        //クリアタイムスクロールの後ボタンスクロール
        if (PosX <= 0)
            CButtonScroll.Scroll();
    }
}
