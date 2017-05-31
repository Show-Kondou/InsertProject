using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTest : MonoBehaviour {

	[Header("フェード番号"), SerializeField]
	FADE    m_FadeNum;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown(KeyCode.Return) ) {
			CSceneManager.Instance.LoadScene( "ShowTest", m_FadeNum );
		}
	}
}
