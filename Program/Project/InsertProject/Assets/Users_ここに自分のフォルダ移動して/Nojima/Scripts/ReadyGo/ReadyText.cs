using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyText : MonoBehaviour
{
    [SerializeField]
    bool bReadyGo = false;  //ReadyGoスタートフラグ（仮）

    [SerializeField]
    GoText[] CGotext;
    [SerializeField]
    FadeText CFadeText;
    [SerializeField]
    GoTextFade CGoTextFade;
    float Alpha = 0f;       //アルファ値

    float Pos_Y = 0f;       //自身の位置
    float InitPosOffset     //初期位置
        = 200f;
    float InitPos_Y = 0f;   //初期位置格納用

    float GoTextTime = 0f;  //Goが出てくるまでの間隔カウント用
    [Header("Goが出てくるまでの間隔"), SerializeField]
    float GoStartCnt = 1f;  //Goが出てくるまでの間隔

    Image MyImage;

    // Use this for initialization
    void Start()
    {
        InitPos_Y = transform.localPosition.y;
        Pos_Y = transform.localPosition.y + InitPosOffset;
        MyImage = GetComponent<Image>();
        MyImage.color = new Color(MyImage.color.r, MyImage.color.g, MyImage.color.b, Alpha);
        transform.localPosition = new Vector3(transform.localPosition.x, Pos_Y);
    }

    // Update is called once per frame
    void Update()
    {
        if (bReadyGo)
            ReadyGoStart(); //ReadyGo演出スタート
    }

    /// <summary>
    /// ReadyGo演出スタート
    /// </summary>
    public void ReadyGoStart()
    {
        if (Pos_Y >= InitPos_Y)
        {
            Alpha += 1f * Time.deltaTime;
            Pos_Y -= 200f * Time.deltaTime; 
            MyImage.color = new Color(MyImage.color.r, MyImage.color.g, MyImage.color.b, Alpha);
            transform.localPosition = new Vector3(transform.localPosition.x, Pos_Y);
        }

        if (Pos_Y <= InitPos_Y)
        {
            GoTextTime += Time.deltaTime;

            //Goテキスト演出開始
            if (GoTextTime >= GoStartCnt)
            {
                Alpha = 0f;
                MyImage.color = new Color(MyImage.color.r, MyImage.color.g, MyImage.color.b, Alpha);

                for (int i = 0; i < 2; i++)
                    CGotext[i].GoTextMove();    //Goテキストの移動処理

                CFadeText.FadeGoText();         //Goテキストのフェード処理
                CGoTextFade.GoTextFadeOut();    //Goテキストのフェード処理
            }
        }
    }
}