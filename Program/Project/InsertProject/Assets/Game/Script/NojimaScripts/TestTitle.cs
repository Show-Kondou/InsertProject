using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTitle : MonoBehaviour {

    Image MyColor;
    float Alpha = 0f;
    bool bAlpha = true;

	// Use this for initialization
	void Start () {
        MyColor = GetComponent<Image>();
        //MyColor.material.color = new Color();
    }
	
	// Update is called once per frame
	void Update () {
        if (Alpha <= 0f)
            bAlpha = true;
        if (Alpha >= 0.5f)
            bAlpha = false;

        if (bAlpha)
            Alpha += 0.01f;
        else
            Alpha -= 0.01f;
        MyColor.material.color = new Color(MyColor.material.color.r, MyColor.material.color.g, MyColor.material.color.b, Alpha);
	}
}
