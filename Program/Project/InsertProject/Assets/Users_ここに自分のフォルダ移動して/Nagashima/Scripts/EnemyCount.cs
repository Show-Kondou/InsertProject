using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCount : MonoBehaviour 
{
    // ----- プライベート変数 -----
    [SerializeField]
    private int nMaxEnemy = 30;     // ゲームオーバー時の敵の最大数
    [SerializeField]
    private int nEnemyCnt = 0;      // とりあえず現在のエネミーの数
    [SerializeField]
    private float fEnemyPosY;       // Y座標格納用
    [SerializeField]
    private bool bGameOver = false; // ゲームオーバーのフラグ

    private GameObject EnemyCntObj; // 敵のカウント数字オブジェクト

    public int nTestEnemy = 0;  // テスト用

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
            Debug.Log("ゲームオーバー");
			CSceneManager.Instance.LoadScene( SCENE.RESULT, FADE.BLACK );
		}
    }

	// ===== スタート関数 =====
	void Start () 
    {
        fEnemyPosY = Mathf.Abs(transform.localPosition.y);

        EnemyCntObj = GameObject.Find("EnemyCnt");
	}
	
	// ===== 更新関数 =====
	void Update () 
    {
        //if (nMaxEnemy > nEnemyCnt)
        //{
        //    nEnemyCnt = nTestEnemy;

        //    this.transform.localScale = new Vector3((float)nEnemyCnt / (float)nMaxEnemy, (float)nEnemyCnt / (float)nMaxEnemy, (float)nEnemyCnt / (float)nMaxEnemy);
            
        //    if(nEnemyCnt > 0)
        //        this.transform.localPosition = new Vector3(0.0f, -fEnemyPosY + (((float)nEnemyCnt / (float)nMaxEnemy) * fEnemyPosY), 0.0f);

        //    EnemyCntObj.GetComponent<Text>().text = nEnemyCnt.ToString();
        //}
        //else
        //{
        //    Debug.Log("ゲームオーバー");
        //}
	}
}
