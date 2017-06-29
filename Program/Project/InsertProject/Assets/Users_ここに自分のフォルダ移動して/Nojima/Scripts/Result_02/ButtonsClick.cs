using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsClick : MonoBehaviour {

    public void TitleButton()
    {
        print("タイトル");
		CSceneManager.Instance.LoadScene(SCENE.TITLE, FADE.BLACK);
    }

    public void RetryButton() {
        print("リトライ");
		CSceneManager.Instance.LoadScene( SCENE.GAME, FADE.BLACK );
	}
}
