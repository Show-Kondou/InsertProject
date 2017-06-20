using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour 
{
    // ----- プライベート変数 -----
    [SerializeField]
    private SCENE LoadSceneName;

    [SerializeField]
    private GameObject TitleBG;         // 背景オブジェクト
    [SerializeField]
    private float fFrameNum = 0;         // フレーム数
    [SerializeField]
    private float fMovePosY = 0;          // 移動量

    private float fMoveSpeed = 0;        // 移動速度
    private float fMoveTime = 0;         // 移動時間

    private float fElapsedTime = 0.0f;   // 経過時間

    private bool bGameStart = false;     // ゲームを開始するフラグ

    // ===== ゲーム開始 =====
    public void GameStart()
    {
        // ボタンを無効化する
        if (this.GetComponent<Button>() != null)
            this.GetComponent<Button>().interactable = false;
		CSoundManager.Instance.PlaySE( AUDIO_LIST.SE_ENTER_1 );

        bGameStart = true;
    }

    // ===== スタート関数 =====
    void Start()
    {
        fMoveTime = (1.0f / 60.0f) * fFrameNum;       // フレーム数から移動時間を計算
        fMoveSpeed = (fMovePosY / fMoveTime);         // 移動量と移動時間から移動速度を計算
    }

    // ===== 更新関数 ======
    void Update()
    {
        if (bGameStart)
        {
            fElapsedTime += Time.deltaTime;  // 経過時間計測

            // 経過時間が移動時間より小さい間
            if (fElapsedTime <= fMoveTime)
            {
                // 背景を移動
                TitleBG.transform.localPosition = Vector3.MoveTowards(TitleBG.transform.localPosition, 
                                                                      TitleBG.transform.localPosition - new Vector3(0, fMovePosY, 0), 
                                                                      fMoveSpeed * Time.deltaTime);
            }
            else
            {
                // シーンチェンジ
                CSceneManager.Instance.LoadScene(LoadSceneName, FADE.BLACK);
            }
        }
    }
}
