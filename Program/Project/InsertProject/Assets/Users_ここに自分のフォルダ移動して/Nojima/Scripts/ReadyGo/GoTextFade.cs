using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoTextFade : MonoBehaviour {

    float Alpha = 1f;
    Image MyImage;

	// Use this for initialization
	void Start () {
		MyImage = GetComponent<Image>();
        MyImage.color = new Color(MyImage.color.r, MyImage.color.g, MyImage.color.b, Alpha);
	}

    public void GoTextFadeOut()
    {
        if (Alpha >= 0f)
        {
            Alpha -= 1f * Time.deltaTime;
            MyImage.color = new Color(MyImage.color.r, MyImage.color.g, MyImage.color.b, Alpha);
        }
    }
}
