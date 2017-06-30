using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoTextFade : MonoBehaviour {

    float Alpha = 1f;
    Image MyImage;

    bool OneProcessing = true;

	[SerializeField]
	Tutorial tutorial;

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

        //フェード終了
        if (Alpha <= 0f && OneProcessing)
        {
			tutorial.Create();
			OneProcessing = false;
        }
    }


}
