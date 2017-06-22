using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAppearManager : MonoBehaviour {

    [SerializeField]
    Image[] FadeImg;            //フェードループなし用
    [SerializeField]
    Image[] LoopFadeImg;        //フェードループ用

    float Alpha = 0f;           //テクスチャのアルファ値

    public bool bStart = false; //ボス登場開始フラグ
    bool bFade = false;         //フェード開始フラグ
    bool bLoopingStop = false;  //フェードのループを止めるフラグ

    float FadeTimeCnt = 0f;     //ボス演出時間カウント用
    [SerializeField]
    float FadeTime = 5f;        //演出時間（秒）
    [SerializeField]
    float FadeSpeed = 0.2f;     //演出の速度

    // Update is called once per frame
    void Update()
    {
        BossProduction();   // ボス登場演出
    }

    /// <summary>
    /// ボス登場演出
    /// </summary>
    void BossProduction()
    {
        ProductionProcessing(); // 演出処理
        ProductionEnd();        // 演出終了

        //フェードループなし用
        if (!bLoopingStop)
        {
            for (int i = 0; i < FadeImg.Length; i++)
                FadeImg[i].material.color =
                    new Color(FadeImg[i].material.color.r, FadeImg[i].material.color.g, FadeImg[i].material.color.b, Alpha);
        }

        //フェードループ用
        for (int i = 0; i < LoopFadeImg.Length; i++)
            LoopFadeImg[i].material.color =
                new Color(LoopFadeImg[i].material.color.r, LoopFadeImg[i].material.color.g, LoopFadeImg[i].material.color.b, Alpha);
    }

    /// <summary>
    /// 演出処理
    /// </summary>
    void ProductionProcessing()
    {
        //FadeTime秒間演出
        if (FadeTimeCnt <= FadeTime)
        {
            //ボスバトルの演出スタート
            if (bStart)
                FadeTimeCnt += Time.deltaTime;

            if (Alpha <= 0f && bStart)
                bFade = true;

            if (Alpha >= 1f)
            {
                bFade = false;
                bLoopingStop = true;
            }
            //フェードイン
            if (bFade)
                Alpha += FadeSpeed * Time.deltaTime;
            //フェードアウト
            else if (Alpha >= 0f)
                Alpha -= FadeSpeed * Time.deltaTime;
        }
    }

    /// <summary>
    /// 演出終了
    /// </summary>
    void ProductionEnd()
    {
        //FadeTime秒後フェードアウト
        if (FadeTimeCnt >= FadeTime)
        {
            bLoopingStop = false;
            bFade = false;

            //フェードアウト
            if (Alpha >= 0f)
                Alpha -= FadeSpeed * Time.deltaTime;

            //演出終了
            if (Alpha <= 0f)
            {
                bStart = false;     //演出開始フラグOFF
                FadeTimeCnt = 0f;   //フェード時間初期化
            }
        }
    }
}
