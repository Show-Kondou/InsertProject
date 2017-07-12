using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour {

    float Alpha = 0f;
    Image MyImage;
    bool bAlphaZero = false;

	// Use this for initialization
	void Start () {
        MyImage = GetComponent<Image>();
	}

    public void FadeGoText()
    {
        if (!bAlphaZero)
        {
            Alpha = 1f;
            MyImage.color = new Color(MyImage.color.r, MyImage.color.g, MyImage.color.b, Alpha);
            bAlphaZero = true;
        }

        if (Alpha >= 0f)
        {
            Alpha -= 2f * Time.deltaTime;
            MyImage.color = new Color(MyImage.color.r, MyImage.color.g, MyImage.color.b, Alpha);
        }
    }
}
