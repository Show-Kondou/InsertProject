using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour {

    [SerializeField]
    float ScaleSpeed = 0.1f;        //拡大スピード
    float ImgSize = 0f;             //クリア画像のサイズ

    [SerializeField]
    TimeScroll CClearTime;

	// Use this for initialization
	void Start () {
        transform.localScale = new Vector2(ImgSize, ImgSize);
	}
	
	// Update is called once per frame
	void Update () {
    }

    /// <summary>
    /// クリア画像拡大
    /// </summary>
    public void ClearScale()
    {
        ImgSize += ScaleSpeed + Time.deltaTime;

        if (ImgSize >= 1)
        {
            ImgSize = 1f;
            CClearTime.Scroll();    //クリアタイムスクロール
        }
        
        transform.localScale = new Vector2(ImgSize, ImgSize);
    }
}
