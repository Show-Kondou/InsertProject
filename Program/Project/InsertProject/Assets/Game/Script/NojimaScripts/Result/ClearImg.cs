using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearImg : MonoBehaviour {

    [SerializeField]
    float ScaleSpeed = 0.1f;        //拡大スピード
    float ImgSize = 0f;             //クリア画像のサイズ

    [SerializeField]
    TimeScroll CClearTime;
    [SerializeField]
    ButtonScroll CButtonScroll;
    [SerializeField]
    ResultFade CResultFade;

    [SerializeField]
    Text ResultText;

    // Use this for initialization
    void Start () {
        transform.localScale = new Vector2(ImgSize, ImgSize);
	}
	
    /// <summary>
    /// クリア画像拡大
    /// </summary>
    public void ClearScale()
    {
        TextChange();   //クリア文字を変える
        ImgSize += ScaleSpeed + Time.deltaTime;

        //クリア画像が最大になったら
        if (ImgSize >= 1)
        {
            ImgSize = 1f;
            //ゲームオーバーだったらクリアタイムを出さない
            if (CResultFade.bGameOver)
                CButtonScroll.Scroll(); //ボタンスクロール
            else
                CClearTime.Scroll();    //クリアタイムスクロール
        }

        transform.localScale = new Vector2(ImgSize, ImgSize);
    }

    /// <summary>
    /// テキスト変更
    /// </summary>
    void TextChange()
    {
        if (CResultFade.bGameOver)
            ResultText.text = "GameOver";
        else
            ResultText.text = "Clear";
    }
}
