using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// TitleBGM再生
		CSoundManager.Instance.PlayBGM( AUDIO_LIST.BGM_TITLE_0, true );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
