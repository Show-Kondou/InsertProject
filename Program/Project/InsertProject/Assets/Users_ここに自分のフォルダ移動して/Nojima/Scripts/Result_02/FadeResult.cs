using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeResult : MonoBehaviour
{

    [SerializeField]
    ResultManager CResultManager;
    [SerializeField]
    ClearProduction CClearProduction;
    [SerializeField]
    float FadeValue = 0.7f;

    float Alpha = 0f;                   //アルファ値
    [SerializeField]
    float FadeSpeed = 0.1f;             //フェードスピード
    Image MyImage;                      //アルファ値変更用

    // Use this for initialization
    void Start()
    {
        MyImage = GetComponent<Image>();
        MyImage.color = new Color(MyImage.color.r, MyImage.color.g, MyImage.color.b, Alpha);
    }

    // Update is called once per frame
    void Update()
    {
        //リザルト開始
        if (CResultManager.bResultStart || CResultManager.bGameOver)
            FadeIn();
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    void FadeIn()
    {
        if (Alpha <= FadeValue)
        {
            GetComponent<RectTransform>().sizeDelta = CResultManager.GetCanvasSize();

            Alpha += FadeSpeed * Time.deltaTime;
            MyImage.color = new Color(MyImage.color.r, MyImage.color.g, MyImage.color.b, Alpha);
        }
    }
}
