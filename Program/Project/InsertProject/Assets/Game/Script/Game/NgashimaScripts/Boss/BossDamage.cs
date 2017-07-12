using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour 
{
    Animator animator;  // アニメーター

    // ----- プライベート変数 -----
    [SerializeField]
    private float fHitPoint = 75;            // ボスのヒットポイント

    private bool bSandwich = true;          // 挟むフラグ
    private float fInvincibleTime = 0.0f;   // 無敵時間

    private bool bSmallDamage = false;      // 小ダメージアニメーションのフラグ
    private bool bBigDamega = false;        // 大ダメージアニメーションのフラグ

    private float fDamageTime = 0.0f;       // ダメージアニメーションカウント

    private GameObject TouchObj;            // タッチオブジェクト
    private GameObject EnemyHPObj;

    // ===== ボスのHPをゲットする関数 =====
    public float GetBossHitPoint()
    {
        return fHitPoint;
    }

    // ===== ボスのHPをセットする関数 =====
    public void SetBossHitPoint(float hp)
    {
        fHitPoint = hp;
    }

    // ===== ダメージを受けた（挟まれた）時の関数 =====
    public void BossHitDamage()
    {
        if (bSandwich)
        {
            Debug.Log("挟まれた");

            CSoundManager.Instance.PlaySE(AUDIO_LIST.SE_BOSS_HIT_0);

            // 待機状態だった場合は通常のダメージモーション
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") == true)
                animator.SetTrigger("Damage");
            // 攻撃状態だった場合はキャンセルモーション
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("BrainControl") == true || animator.GetCurrentAnimatorStateInfo(0).IsName("Summon") == true)
                animator.SetTrigger("Cancel");

            // 動きを止める
            this.GetComponent<BossMove>().SetMoveNum(0);

			Debug.Log(CSameSandwichAction.m_BossDamage);	// 同時挟まれ数取得

            if (CSameSandwichAction.m_BossDamage <= 10)
            {
                fHitPoint -= CSameSandwichAction.m_BossDamage;
                EnemyHPObj.GetComponent<EnemyGauge>().AddDamage(CSameSandwichAction.m_BossDamage);
            }
            else
            {
                fHitPoint -= 10;
                EnemyHPObj.GetComponent<EnemyGauge>().AddDamage(10);
            }
      
            bSandwich = false;
        }
    }

	// ===== スタート関数 =====
	void Start () 
    {
        // アニメーターを格納
        animator = GetComponent<Animator>();

        TouchObj = GameObject.Find("Touch");

        // ボスのHPバー関係
        EnemyHPObj = GameObject.Find("UIs").transform.FindChild("EnemyHP").gameObject;
        EnemyHPObj.GetComponent<EnemyGauge>().SetHP(this.GetComponent<BossDamage>().GetBossHitPoint());
        EnemyHPObj.SetActive(true);
	}
	
	// ===== 更新関数 =====
	void Update () 
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Damage") == true)
        {
            fDamageTime += Time.deltaTime;

            if (fDamageTime >= animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                fDamageTime = 0.0f;
                animator.SetTrigger("Damage");
            }
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Cancel") == true)
        {
            fDamageTime += Time.deltaTime;

            if (fDamageTime >= animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                fDamageTime = 0.0f;
                animator.SetTrigger("Cancel");
            }
        }
            

        if (!bSandwich)
        {
            fInvincibleTime += Time.deltaTime;

            if (fInvincibleTime >= 2.0f)
            {
                // ボスを動かす
                this.GetComponent<BossMove>().SetMoveNum(Random.Range(1, 3));

                bSmallDamage = false;
                bBigDamega = false;
                bSandwich = true;
                fDamageTime = 0.0f;
                fInvincibleTime = 0.0f;
            }
        }
	}
}
