using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsClick : MonoBehaviour {

    public void TitleButton()
    {
		CSceneManager.Instance.LoadScene(SCENE.TITLE, FADE.BLACK);
    }

    public void RetryButton() {
		CSceneManager.Instance.LoadScene( SCENE.GAME, FADE.BLACK );
	}
}
