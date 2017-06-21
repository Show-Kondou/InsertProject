using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGScroll : MonoBehaviour
{
    //定数
    const int MAX_PANEL = 2;                        //ヒエラルキーのPanelの数

    //変数
    float SCROLL_SPEED = -2f;                       //スクロールスピード

    [SerializeField]
    bool bSwichingStage = false;                    //ステージ切り替え
    bool[] bTerms = new bool[MAX_PANEL];
    
    [SerializeField]
    GameObject[] BGObj = new GameObject[MAX_PANEL]; //背景オブジェクト
    Renderer[] Rend = new Renderer[MAX_PANEL];
    Image[] BGImg = new Image[MAX_PANEL];
    RectTransform PanelSize;                        //Panel一枚のサイズ

    [SerializeField]
    Sprite[] StageTexture;                          //ステージのテクスチャ
    int[] TextureCnt = new int[MAX_PANEL];          //テクスチャ配列のカント用

    int PanelNumberStorage = 0;                     //パネル番号格納用
    bool bStrage = false;                           //一度だけ格納するよう
    
    RectTransform CanvasSize;                       //キャンバスのサイズ取得用

    [SerializeField]
    SwitchingImage SwitchingCanvas;
    public int SwitchCnt = 0;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < MAX_PANEL; i++)
        {
            TextureCnt[i] = 0;
            bTerms[i] = false;

            //テクスチャをPanelにセット
            BGImg[i] = BGObj[i].GetComponent<Image>();
            BGImg[i].sprite = StageTexture[TextureCnt[i]];

            //Panelをキャンバスのサイズにする
            PanelSize = BGObj[i].GetComponent<RectTransform>();
            CanvasSize = GetComponent<RectTransform>();
            PanelSize.sizeDelta = new Vector2(CanvasSize.sizeDelta.x, CanvasSize.sizeDelta.y);
        }
     }

    // Update is called once per frame
    void Update()
    {
        //2枚のPanelをスクロール
        for(int i = 0; i < MAX_PANEL; i++)
            Scroll(BGObj[i], i);

        //ステージ切り替え
        if (bSwichingStage)
        {
            SwitchingCanvas.SetScrollSpeed();
            bStrage = true;
            for (int j = 0; j < MAX_PANEL; j++)
                bTerms[j] = true;
            bSwichingStage = false;
        }

        if ((SwitchCnt % 2) != 0)
            SwitchingCanvas.Scroll();
    }

    /// <summary>
    /// スクロール処理
    /// </summary>
    /// <param name="BGObj"></param>
    /// <param name="PanelNumber"></param>
    void Scroll(GameObject BGObj, int PanelNumber)
    {
        //Panelスクロール
        BGObj.transform.Translate(0f, SCROLL_SPEED, 0);

        //画面外まで行ったら上に戻す
        if (BGObj.transform.localPosition.y <= -PanelSize.sizeDelta.y + 20f)
        {
            BGObj.transform.localPosition = new Vector3(BGObj.transform.localPosition.x, PanelSize.sizeDelta.y, BGObj.transform.localPosition.z);

            //ステージ切り替え
            if (bTerms[PanelNumber])
                SwitchingBG(PanelNumber);
        }
    }
    
    /// <summary>
    /// テクスチャ変更処理
    /// </summary>
    /// <param name="PanelNumber"></param>
    void SwitchingBG(int PanelNumber)
    {
        if (bStrage)
        {
            PanelNumberStorage = PanelNumber;   //パネルナンバー格納
            bStrage = false;                    
        }

        //切り替え画像処理
        if (PanelNumber == PanelNumberStorage)  
        {
            SwitchCnt ++;

            TextureCnt[PanelNumber]++;      //次のテクスチャに
            if ((TextureCnt[PanelNumber] % 2) == 0 && TextureCnt[PanelNumber] != 0)
                bTerms[PanelNumber] = false;
            if ((TextureCnt[PanelNumber] % 4) == 0)
                SCROLL_SPEED = 0f;
        }
        //ステージ切り替え用処理
        else
        {
            TextureCnt[PanelNumber] += 2;  //次のステージに
            bTerms[PanelNumber] = false;
        }

        BGImg[PanelNumber].sprite = StageTexture[TextureCnt[PanelNumber]];
    }
}