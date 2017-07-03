using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScreenSize : MonoBehaviour {
	float width  = 540;
	float height = 960;

	bool fullscreen = false;

	int preferredRefreshRate = 60;

	// Use this for initialization
	void Start () {
		Screen.SetResolution((int)width , (int)height, fullscreen, preferredRefreshRate);
	}
	
	// Update is called once per frame
	void Update () {
		Screen.SetResolution((int)width, (int)height, fullscreen, preferredRefreshRate);
	}
}
