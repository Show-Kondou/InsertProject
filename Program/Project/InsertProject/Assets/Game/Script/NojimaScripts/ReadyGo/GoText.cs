using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoText : MonoBehaviour
{
    [SerializeField]
    float MaxScale = 1.5f;
    [SerializeField]
    float ScaleSpeed = 5f;
    float MyScale = 1f;
    Image MyImage;
    float Alpha = 0f;

    bool bAlphaZero = false;
    void Start()
    {
        MyImage = GetComponent<Image>();
        MyImage.color = new Color(MyImage.color.r, MyImage.color.g, MyImage.color.b, 0f);
        transform.localScale = new Vector3(MyScale, MyScale);
    }

    public void GoTextMove()
    {
        if (!bAlphaZero)
        {
            MyImage.color = new Color(MyImage.color.r, MyImage.color.g, MyImage.color.b, 1f);
            bAlphaZero = true;
        }
        if (MyScale <= MaxScale)
        {
            MyScale += ScaleSpeed * Time.deltaTime;
            transform.localScale = new Vector3(MyScale, MyScale);
        }
    }
}
