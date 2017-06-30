using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCount : MonoBehaviour 
{
    // ----- プライベート変数 -----
    [SerializeField]
    private int nMaxEnemy = 50;     // ゲームオーバー時の敵の最大数
    [SerializeField]
    private int nEnemyCnt = 0;      // とりあえず現在のエネミーの数
    [SerializeField]
    private float fEnemyPosY;       // Y座標格納用

    [Header("スライム多すぎゲームオーバー"), SerializeField]
    private ResultManager resultMgr;

    private GameObject EnemyCntObj; // 敵のカウント数字オブジェクト

    private bool bYellow = false;
    private bool bRed = false;
    private bool bLastCnt = false;

    private bool bColorReversal = false;
    private float fColorChangeSpeed = 1.0f;

    // ===== 敵の数をカウントする関数 =====
    public void EnemyCnt(int enemycnt)
    {
        // 敵の数が最大に達するまで
        if (nMaxEnemy > nEnemyCnt)
        {
            nEnemyCnt = enemycnt;   // 敵の数を代入

            // 敵の数によってキングスライムのサイズを変更
            this.transform.localScale = new Vector3((float)nEnemyCnt / (float)nMaxEnemy, (float)nEnemyCnt / (float)nMaxEnemy, (float)nEnemyCnt / (float)nMaxEnemy);

            // 敵の数によってキングスライムの位置を変更
            if (nEnemyCnt > 0)
                this.transform.localPosition = new Vector3(0.0f, -fEnemyPosY + (((float)nEnemyCnt / (float)nMaxEnemy) * fEnemyPosY), 0.0f);

            // 敵の数を変更
            EnemyCntObj.GetComponent<Text>().text = nEnemyCnt.ToString();
        }
        else
        {
            resultMgr.bGameOver = true;

            //CSceneManager.Instance.LoadScene( SCENE.RESULT, FADE.BLACK );
		}
    }

	// ===== スタート関数 =====
	void Start ()
    {
        CSlimeMove.EnemyNum = 0;
        fEnemyPosY = Mathf.Abs(transform.localPosition.y);

        EnemyCntObj = GameObject.Find("EnemyCnt");
        if ( !resultMgr ) {
            Debug.LogError("リザルトマネージャーが設定されてない");
        }
	}
	
	// ===== 更新関数 =====
	void Update () 
    {
		EnemyCnt( CSlimeMove.EnemyNum );

        // 文字を黄色にする処理
        if (nEnemyCnt == nMaxEnemy / 2)
        {
            EnemyCntObj.GetComponent<Text>().color = new Color(255, 255, 0, 255);
            EnemyCntObj.GetComponent<Outline>().effectColor = new Color(0, 0, 0, 255);

            bYellow = true;
        }
        if(nEnemyCnt < nMaxEnemy / 2 && bYellow)
        {
            EnemyCntObj.GetComponent<Text>().color = new Color(0, 0, 0, 255);
            EnemyCntObj.GetComponent<Outline>().effectColor = new Color(255, 255, 255, 255);

            bYellow = false;
        }

        // 文字を赤色にする処理
        if (nEnemyCnt == nMaxEnemy - (nMaxEnemy / 5) - 10)
        {
            EnemyCntObj.GetComponent<Text>().color = new Color(255, 0, 0, 255);
            bRed = true;
        }
        if (nEnemyCnt < nMaxEnemy - (nMaxEnemy / 5) - 10 && bRed)
        {
            EnemyCntObj.GetComponent<Text>().color = new Color(255, 255, 0, 255);

            bRed = false;
        }

        // 90以上の演出
        if (nEnemyCnt == nMaxEnemy - (nMaxEnemy / 5))
            bLastCnt = true;

        if (nEnemyCnt >= nMaxEnemy - (nMaxEnemy / 5))
        {
            if (!bColorReversal)
            {
                EnemyCntObj.GetComponent<Text>().color += new Color(0, fColorChangeSpeed / 2, 0, 0) * Time.deltaTime;
                EnemyCntObj.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f) * Time.deltaTime;

                if (EnemyCntObj.GetComponent<Text>().color.g >= 0.5f)
                {
                    bColorReversal = true;
                }
            }
            else
            {
                EnemyCntObj.GetComponent<Text>().color -= new Color(0, fColorChangeSpeed / 2, 0, 0) * Time.deltaTime;
                EnemyCntObj.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f) * Time.deltaTime;

                if (EnemyCntObj.GetComponent<Text>().color.g <= 0)
                {
                    bColorReversal = false;
                }
            }
        }
        if (nEnemyCnt <= nMaxEnemy - ((nMaxEnemy / 5) - 1) && bLastCnt)
        {
            EnemyCntObj.GetComponent<Text>().color = new Color(255, 0, 0, 255);
            EnemyCntObj.transform.localScale = new Vector3(1, 1, 1);

            bLastCnt = false;
        }
	}
}
