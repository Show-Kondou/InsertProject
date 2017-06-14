using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		CSoundManager.Instance.PlayBGM( AUDIO_LIST.BGM_MAIN, true );	
	}
	
	// Update is called once per frame
	void OnDestroy() {
		CSoundManager.Instance.StopBGM( AUDIO_LIST.BGM_MAIN );
	}
}
