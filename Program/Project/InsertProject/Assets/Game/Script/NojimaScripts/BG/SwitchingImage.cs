using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchingImage : MonoBehaviour
{
    const float MAX_SPEED = -0.8f;

    [SerializeField]
    GameObject SwitchingPanel;
    RectTransform ImageSize;
    Image switchingImage;

    [SerializeField]
    BGProcess bgProcess;

    [SerializeField]
    Sprite[] SwitchTexture;

    RectTransform CanvasSize;
    float ScrollSpeed = MAX_SPEED;

    bool bStop = true;

    // Use this for initialization
    void Start () {
        CanvasSize = GetComponent<RectTransform>();
        ImageSize = SwitchingPanel.GetComponent<RectTransform>();

        //テクスチャセット
        switchingImage = SwitchingPanel.GetComponent<Image>();
        switchingImage.sprite = SwitchTexture[0];

        //テクスチャをCanvasと同じサイズにする
        ImageSize.sizeDelta = new Vector2(CanvasSize.sizeDelta.x, CanvasSize.sizeDelta.y);
        SwitchingPanel.transform.localPosition = new Vector2(SwitchingPanel.transform.localPosition.x, ImageSize.sizeDelta.y);
    }
    
    public void Scroll()
    {
        SwitchingPanel.transform.Translate(0f, ScrollSpeed * 2, 0);
        //画面外まで行ったら上に戻す
        if (SwitchingPanel.transform.localPosition.y <= -ImageSize.sizeDelta.y)
        {
            SwitchingPanel.transform.localPosition =
                new Vector3(SwitchingPanel.transform.localPosition.x, ImageSize.sizeDelta.y, SwitchingPanel.transform.localPosition.z);
            if (SwitchingPanel.transform.localPosition.y >= ImageSize.sizeDelta.y)
                bgProcess.SwitchCnt = 1;
            switchingImage.sprite = SwitchTexture[1];
            if(bStop)
                ScrollSpeed = 0f;
        }
    }
    public void SetScrollSpeed()
    {
        bStop = false;
        ScrollSpeed = MAX_SPEED;
        print(ScrollSpeed);
    }
}