using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimit : MonoBehaviour 
{
    // ===== プライベート変数 =====
    [SerializeField]
    private float fOneRoundTime = 60.0f; // 一周するのにかかる時間

	[Header("ボスの生成される時間"), SerializeField]
	private float fCreateBossTime = 10.0F;
	[Header("ボス出現UI"), SerializeField]
	private BossAppearManager BossUI;

    [Header("ゲームオーバー演出"), SerializeField]
    private ResultManager resultMgr;

	private bool isCreateBoss = false;

	// どこを軸に回るかのフラグ
	[SerializeField]
    private bool bRotateX = false;
    [SerializeField]
    private bool bRotateY = false;
    [SerializeField]
    private bool bRotateZ = true;

    // 時間を表示するフラグ
    private bool bTimeDisplay = false;
    // ゲームオーバーフラグ
    private bool bGameOver = false;
    // 時計の色を表示するフラグ
    private int nColorChange = 0;
    // 一秒で動く角度
    private float fOneSecondRotate = 0.0f;
    // 制限時間測定
    private float fLimitTime = 0.0f;

    // 時計オブジェクト格納用
    private GameObject ClockObj;
    // タイマーオブジェクト格納用
    private GameObject TimerObj;

    // ===== 一周の時間を取得する関数 =====
    public float GetLimitTime()
    {
        return fLimitTime;
    }

    // ===== スタート関数 =====
    void Start()
    {
        // 一周の秒数を格納
        fLimitTime = fOneRoundTime;
		fCreateBossTime = fLimitTime - fCreateBossTime;

        // 一秒で動く角度を計算
        fOneSecondRotate = 360.0f / fOneRoundTime;

        ClockObj = GameObject.Find("Clock");
        TimerObj = ClockObj.transform.FindChild("Timer").gameObject;

        ClockObj.GetComponent<Image>().material.color = Color.white;
        if ( !resultMgr ) { Debug.LogError("リザルトマネージャーがないよ"); }
    }

    // ===== 更新関数 =====
    void Update()
    {
        fLimitTime -= Time.deltaTime;

        if (!bGameOver)
        {
            // 回転
            transform.Rotate((bRotateX) ? -fOneSecondRotate * Time.deltaTime : 0.0f,
                             (bRotateY) ? -fOneSecondRotate * Time.deltaTime : 0.0f,
                             (bRotateZ) ? -fOneSecondRotate * Time.deltaTime : 0.0f);
        }

        // 30秒以下になったら残り時間を表示
        if (fLimitTime <= fOneRoundTime - 30.0f && !bTimeDisplay)
        {
            bTimeDisplay = true;

            TimerObj.SetActive(true);   // 時間表示
            // Debug.Log("時間表示");
        }

        // 針が半周したら時計を黄色に
        if (fLimitTime <= fOneRoundTime / 2 && nColorChange == 0)
        {
            nColorChange = 1;

            ClockObj.GetComponent<Image>().color = Color.yellow;
            // Debug.Log("黄色にする");
        }
        // 針が3/4周したら時計を赤色に
        if (fLimitTime <= (fOneRoundTime / 4) && nColorChange == 1)
        {
            nColorChange = 2;
            ClockObj.GetComponent<Image>().color = Color.red;
            // Debug.Log("赤色にする");
        }
        // 針が一周したらゲームオーバー
        if (fLimitTime <= 0.0f && !bGameOver)
        {
            bGameOver = true;

            TimerObj.SetActive(false);  // 時間非表示
            resultMgr.bTimeOver = true;
			// CSceneManager.Instance.LoadScene( SCENE.RESULT, FADE.BLACK );
        }

		if( fLimitTime <= fCreateBossTime && !isCreateBoss ) {
			BossUI.bStart = true;
			CSoundManager.Instance.StopBGM();
			isCreateBoss = true;
		}

    }
}
