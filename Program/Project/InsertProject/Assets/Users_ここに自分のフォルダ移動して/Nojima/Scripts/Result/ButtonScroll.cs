using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScroll : MonoBehaviour {
    
    float PosY = 0f;            //Y座標位置
    float StopPosY = 0f;        //スクロールの停止位置
    [SerializeField]
    TimeScroll CTimeScroll;     //スクロールスピード取得用
    [SerializeField]
    Canvas CCanvas;

	// Use this for initialization
	void Start () {
        //初期位置
        StopPosY = transform.localPosition.y;
        PosY = -((CCanvas.GetComponent<CanvasScaler>().referenceResolution.y / 2f) + (GetComponent<RectTransform>().sizeDelta.y));
        transform.localPosition = new Vector2(transform.localPosition.x, PosY);
    }
	
    /// <summary>
    /// 上にスクロール
    /// </summary>
    public void Scroll() {

        if (PosY <= StopPosY)
        {
            PosY += CTimeScroll.ScrollSpeed * Time.deltaTime;
            transform.localPosition = new Vector2(transform.localPosition.x, PosY);
        }
    }
}
