using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour {

    public bool bResultStart = false;   //リザルトスタート
    public bool bGameOver = false;
    public bool bTimeOver = false;
    [System.NonSerialized]
    public bool bResultMenu = false;
    [System.NonSerialized]
    public bool bTimeOverEnd = false;
    BossCameraMove CBossCameraMove;
    bool bBoss = true;

    [SerializeField]
    BossAppearManager CBossAppearManager;

	[Header("ストップイベント"), SerializeField]
	private StopGame stopGame;


    [SerializeField]
    Canvas CCanvas;                     //キャンバスのサイズ取得用
    Vector2 CanvasSize;                 //キャンバスのサイズ取得用

	// Use this for initialization
	void Start () {
        CanvasSize = CCanvas.GetComponent<RectTransform>().sizeDelta;
        CSoundManager.Instance.StopBGM();
        CSoundManager.Instance.PlayBGM( AUDIO_LIST.BGM_GAMEOVER );
		if( !stopGame ) {
			Debug.LogError( "ストップゲームが取得できていない" );
		}
    }

    public Vector2 GetCanvasSize()
    {
        return CanvasSize;
    }

	void Update() {
        if (CBossAppearManager.BossObj != null && bBoss)
        {
            CBossCameraMove = CBossAppearManager.BossObj.GetComponent<BossCameraMove>();
            bBoss = false;
        }
        if (CBossCameraMove != null)
        {
            if (CBossCameraMove.GetFinishFlg())
                bResultStart = true;
        }

		bool isEvent = bResultStart || bTimeOver || bGameOver;
		if( isEvent ) {
			stopGame.StopGameEvent();
		} else {
			stopGame.StartGameEvent();
		}
	}

}
