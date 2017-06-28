using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultFade : MonoBehaviour {

    public bool bResultStart = false;   //リザルト開始フラグ
    public bool bGameOver = false;      //ゲームオーバーフラグ（仮）
    float Alpha = 0f;                   //アルファ値
    [SerializeField]
    float FadeSpeed = 0.1f;             //フェードスピード
    Image MyImage;                      //アルファ値変更用

    [SerializeField]
    ClearImg CClear;

    [SerializeField]
    Canvas CCanvas;

	// Use this for initialization
	void Start () {
        GetComponent<RectTransform>().sizeDelta = CCanvas.GetComponent<RectTransform>().sizeDelta;
        MyImage = GetComponent<Image>();
        MyImage.color = new Color(MyImage.color.r, MyImage.color.g, MyImage.color.b, Alpha);
	}
	
	// Update is called once per frame
	void Update () {
        //リザルト開始
        if(bResultStart)
            FadeIn();
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    void FadeIn()
    {
        if (Alpha <= 0.7f)
        {
            Alpha += FadeSpeed * Time.deltaTime;
            MyImage.color = new Color(MyImage.color.r, MyImage.color.g, MyImage.color.b, Alpha);
        }
        
        //クリア画像拡大
        if (Alpha >= 0.7f)
            CClear.ClearScale();
    }
}
