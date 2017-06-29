using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsClick : MonoBehaviour {

    public void TitleButton()
    {
        CSoundManager.Instance.PlaySE(AUDIO_LIST.SE_ENTER_0);

		CSceneManager.Instance.LoadScene(SCENE.TITLE, FADE.BLACK);
    }

    public void RetryButton() {
        CSoundManager.Instance.PlaySE(AUDIO_LIST.SE_ENTER_0);

		CSceneManager.Instance.LoadScene( SCENE.GAME, FADE.BLACK );
	}
}
