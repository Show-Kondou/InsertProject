using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour 
{
    Animator animator;  // アニメーター

    // ----- プライベート変数 -----
    [SerializeField]
    private int nScreenWidth;
    [SerializeField]
    private int nScreenHeight;

    private GameObject SandwichObjectManagerObj;            // 召喚オブジェクト
    private CSSandwichObjManager _CSSandwichObjManager;     // 召喚スクリプト

    [SerializeField]
    private GameObject WormholeObj;                         // ワームホールオブジェクト
    private Vector3 WormholePos;                            // ワームホール座標

    private float SummonIntervalTime = 0.0f;                // 召喚時間間隔
    [SerializeField]
    private float SummonOneTime = 0.2f;                     // 一体の召喚時間
    [SerializeField]
    private int SummonNum = 25;                             // 召喚回数
    private int StartSummonNum = 0;                         // 初めの召喚回数

    private bool bWormhole = false;
    
    [SerializeField]
    private List<GameObject> m_AllySlimeObj = new List<GameObject>(); // 洗脳する味方スライムを格納するリスト

    private bool bOneControl = false;                               // 洗脳攻撃のフラグ

    [SerializeField]
    private int AttackNum = 0;                              // 攻撃パターンの番号
    [SerializeField]
    private float AttackInterval = 0;                       // 一回の攻撃間隔

    [SerializeField]
    private float WaitTime = 0;

    [SerializeField]
    private GameObject Particletest;    // Particleテスト用

	// ===== スタート関数 =====
	void Start () 
    {
        // アニメーターを格納
        animator = GetComponent<Animator>();

        // モンスター生成関連
        SandwichObjectManagerObj = GameObject.Find("SandwichObjectManager");
        _CSSandwichObjManager = SandwichObjectManagerObj.GetComponent<CSSandwichObjManager>();

        // 初めの召喚回数
        StartSummonNum = SummonNum;
    }
	
	// ===== 更新関数 =====
    void Update()
    {
        AttackInterval += Time.deltaTime;

        // 10秒毎にどちらかの攻撃を実行
        if (AttackInterval >= 10.0f)
        {
            this.GetComponent<BossMove>().SetMoveNum(0);    // ボスを停止させる

            AttackNum = Random.Range(1, 3);                 // 召喚か洗脳を行う
            AttackInterval = 0.0f;
        }

        switch (AttackNum)
        {
            case 1: // 召喚
                
                WaitTime += Time.deltaTime; // ボスが停止している時間

                if (!bWormhole)
                {
                    animator.SetTrigger("Summon");

                    Instantiate(WormholeObj, new Vector2(Random.Range(-2, 3), Random.Range(-4, 5)), Quaternion.identity); // ワームホール生成
                    WormholePos = GameObject.Find("Wormhole(Clone)").transform.localPosition;                             // ワームホールの座標を格納
                    bWormhole = true;
                }

                SummonIntervalTime += Time.deltaTime;
                
                // 召喚間隔毎に召喚
                if (SummonIntervalTime >= SummonOneTime && SummonNum > 0)
                {
                    SummonNum--;
                    SummonIntervalTime = 0.0f;
                
                    _CSSandwichObjManager.CreateSandwichObj(0, new Vector2(WormholePos.x, WormholePos.y));  // ワームホールの上にスライム生成
                    
                    // 全ての召喚が終わったら
                    if (SummonNum == 0)
                    {
                        SummonNum = StartSummonNum;
                        bWormhole = false;               
                        Destroy(GameObject.Find("Wormhole(Clone)"));
                        AttackNum = 0;
                    }
                }

                if (WaitTime >= animator.GetCurrentAnimatorStateInfo(0).length)
                {
                    this.GetComponent<BossMove>().SetMoveNum(Random.Range(1, 3));    // ボスを動かす
                    WaitTime = 0.0f;
                }

                break;
            case 2: // 洗脳

                WaitTime += Time.deltaTime; // ボスが停止している時間

                if (!bOneControl)
                {
                    animator.SetTrigger("BrainControl");

                    m_AllySlimeObj.AddRange(GameObject.FindGameObjectsWithTag("Ally"));    // 洗脳対象の味方スライムをリストに格納

                    if (m_AllySlimeObj.Count == 1)
                    {
                        // 洗脳処理

                        // 洗脳対象位置にパーティクルを出現させる（テスト）
                        Instantiate(Particletest, new Vector3(m_AllySlimeObj[0].transform.localPosition.x, m_AllySlimeObj[0].transform.localPosition.y, m_AllySlimeObj[0].transform.localPosition.z - 0.3f), Quaternion.identity);
                    }
                    if (m_AllySlimeObj.Count >= 2)
                    {
                        // 洗脳処理

                        // 洗脳対象位置にパーティクルを出現させる（テスト）
                        Instantiate(Particletest, new Vector3(m_AllySlimeObj[0].transform.localPosition.x, m_AllySlimeObj[0].transform.localPosition.y, m_AllySlimeObj[0].transform.localPosition.z - 0.3f), Quaternion.identity);
                        Instantiate(Particletest, new Vector3(m_AllySlimeObj[1].transform.localPosition.x, m_AllySlimeObj[1].transform.localPosition.y, m_AllySlimeObj[1].transform.localPosition.z - 0.3f), Quaternion.identity);

                    }

                    m_AllySlimeObj.Clear(); // リスト内を削除
                    bOneControl = true;
                }

                if (WaitTime >= animator.GetCurrentAnimatorStateInfo(0).length)
                {
                    this.GetComponent<BossMove>().SetMoveNum(Random.Range(1, 3));    // ボスを動かす
                    AttackNum = 0;
                    WaitTime = 0.0f;
                    bOneControl = false;
                }

                break;
        }
    }
}
