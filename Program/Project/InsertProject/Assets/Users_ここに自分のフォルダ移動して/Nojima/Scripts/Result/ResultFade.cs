using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultFade : MonoBehaviour {

    public bool bResult = false;    //リザルト開始フラグ

    float Alpha = 0f;               //アルファ値
    [SerializeField]
    float FadeSpeed = 0.1f;         //フェードスピード
    Image MyImage;

    [SerializeField]
    Clear CClear;

	// Use this for initialization
	void Start () {
        transform.localScale = new Vector2(Screen.width, Screen.height);
        MyImage = GetComponent<Image>();
        MyImage.color = new Color(MyImage.color.r, MyImage.color.g, MyImage.color.b, Alpha);
	}
	
	// Update is called once per frame
	void Update () {
        //リザルト開始
        if(bResult)
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
